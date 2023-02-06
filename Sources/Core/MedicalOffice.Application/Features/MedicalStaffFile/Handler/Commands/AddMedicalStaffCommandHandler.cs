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
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMedicalStaffRoleRepository _medicalStaffRoleRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly string _requestTitle;

        public AddMedicalStaffCommandHandler(
            IValidator<MedicalStaffDTO> validator,
            IMapper mapper,
            ILogger logger,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IMedicalStaffRepository medicalStaffrepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IMedicalStaffRoleRepository medicalStaffRoleRepository,
            IOfficeRepository officeRepository,
            IUserRepository userRepository
            )
        {
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffrepository;
            _medicalStaffRoleRepository = medicalStaffRoleRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;

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

            //Check staff is exist`
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
            var user = await _userRepository.CheckByPhoneOrNationalId(request.DTO.PhoneNumber, request.DTO.NationalID);

            //Create user is not exist
            if (user == null)
            {
                var newUser = _mapper.Map<User>(request.DTO);
                newUser.Id = Guid.NewGuid();
                newUser.UserName = request.DTO.PhoneNumber;
                newUser.NormalizedUserName = request.DTO.PhoneNumber;

                var userCreation = await _userManager.CreateAsync(newUser);
                if (userCreation.Succeeded)
                {
                    user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                }
                else
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
            }

            //Create medicalStaff
            var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            medicalStaff.UserId = user.Id;
            medicalStaff.OfficeId = request.OfficeId;
            medicalStaff = await _medicalStaffrepository.Add(medicalStaff);

            //Add role to user office roles
            var roleName = new List<string>();
            var userOfficeRoles = new List<UserOfficeRole>();
            var medicalstaffRole = new List<MedicalStaffRole>();
            if (request.DTO.RoleIds != null)
            {
                foreach (var roleId in request.DTO.RoleIds)
                {
                    Role role = await _roleManager.FindByIdAsync(roleId.ToString());
                    if (role != null)
                    {
                        userOfficeRoles.Add(new UserOfficeRole
                        {
                            RoleId = roleId,
                            UserId = user.Id,
                            OfficeId = request.OfficeId
                        });

                        medicalstaffRole.Add(new MedicalStaffRole
                        {
                            RoleId = role.Id,
                            MedicalStaffId = medicalStaff.Id
                        });

                        roleName.Add(role.NormalizedName);
                    }
                }
                await _userOfficeRoleRepository.AddUserOfficeRoles(userOfficeRoles);
                await _medicalStaffRoleRepository.InsertToMedicalStaffRole(medicalstaffRole);
                await _userManager.AddToRolesAsync(user, roleName);
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