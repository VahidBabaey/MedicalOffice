using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.LogicProviders;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{
    public class AddMedicalStaffCommandHandler : IRequestHandler<AddMedicalStaffCommand, BaseResponse>
    {
        private readonly IValidator<MedicalStaffDTO> _validator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;
        private readonly string _requestTitle;

        public AddMedicalStaffCommandHandler(
            IValidator<MedicalStaffDTO> validator,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IMapper mapper,
            ILogger logger,
            IMedicalStaffRepository medicalStaffrepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IUserRepository userRepository,
            IRolePermissionRepository rolePermissionRepository,
            IUserOfficePermissionRepository userOfficePermissionRepository
            )
        {
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffrepository;
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _userOfficePermissionRepository = userOfficePermissionRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            //Validate
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }


            //Check staff is exist
            var isMedicalStaffExist = await _medicalStaffrepository.CheckExistByOfficeIdAndPhoneNumber(
                request.OfficeId, request.DTO.PhoneNumber);

            if (isMedicalStaffExist)
            {
                var error = $"There is a medical staff with this phoneNumber in this office";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            //Check user is exist
            var user = new User();
            var userByPhoneNumber = await _userRepository.GetUserByPhoneNumber(request.DTO.PhoneNumber);
            var userByNationalId = await _userRepository.GetUserByNationalId(request.DTO.NationalId);

            //condistions
            var bothNotExist = userByPhoneNumber == null && userByNationalId == null;
            var bothExist = userByPhoneNumber != null && userByNationalId != null;
            var phoneNumberExist = userByPhoneNumber != null || userByNationalId == null;
            var nationalIdExist = userByPhoneNumber == null || userByNationalId != null;

            if (bothNotExist)
            {
                var newUser = _mapper.Map<User>(request.DTO);
                newUser.UserName = request.DTO.PhoneNumber;
                newUser.NormalizedUserName = request.DTO.PhoneNumber;

                var userCreation = await _userManager.CreateAsync(newUser);
                if (!userCreation.Succeeded)
                {
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = userCreation.Errors.Select(x => x.Description)
                    }); ;
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", userCreation.Errors.Select(x => x.Description).ToArray());
                }

                user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
            }
            else if (bothExist)
            {
                if (userByPhoneNumber.Id == userByNationalId.Id)
                {
                    user = userByPhoneNumber;
                }
                else
                {
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = " شماره تماس و کدملی متعلق به کاربر دیگری است."
                    }); ;
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", " شماره تماس و کدملی متعلق به کاربر دیگری است.");
                }
            }
            else if (phoneNumberExist)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = " شماره تماس متعلق به کاربر دیگری است"
                }); ;
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", " شماره تماس متعلق به کاربر دیگری است");
            }
            else if (nationalIdExist)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = " کدملی متعلق به کاربر دیگری است"
                }); ;
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", " کدملی متعلق به کاربر دیگری است");
            }

            //Create medicalStaff
            var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            medicalStaff.UserId = user.Id;
            medicalStaff.OfficeId = request.OfficeId;
            medicalStaff = await _medicalStaffrepository.Add(medicalStaff);

            //Add role to user office roles
            await _userOfficeRoleRepository.Add(new UserOfficeRole
            {
                RoleId = request.DTO.RoleId,
                UserId = user.Id,
                OfficeId = request.OfficeId
            });

            var permissions = await _rolePermissionRepository.GetByRoleId(request.DTO.RoleId);
            var userOfficePermissions = new List<UserOfficePermission>();
            foreach (var item in permissions)
            {
                userOfficePermissions.Add(new UserOfficePermission
                {
                    UserId = user.Id,
                    OfficeId = request.OfficeId,
                    PermissionId = item.Id
                });
            }
            await _userOfficePermissionRepository.AddRange(userOfficePermissions);

            var role = await _roleManager.FindByIdAsync(request.DTO.RoleId.ToString());
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.NormalizedName);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = medicalStaff.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", medicalStaff.Id);
        }
    }
}