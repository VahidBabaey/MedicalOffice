using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.LogicProviders;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators;
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
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMedicalStaffRoleRepository _medicalStaffRoleRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IMedicalStaffPermissionRepository _mediicalStaffPermissionRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly string _requestTitle;

        public AddMedicalStaffCommandHandler(
            IMapper mapper,
            ILogger logger,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IMedicalStaffRepository medicalStaffrepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IMedicalStaffRoleRepository medicalStaffRoleRepository, 
            IMedicalStaffPermissionRepository mediicalStaffPermissionRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffrepository;
            _medicalStaffRoleRepository = medicalStaffRoleRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
            _mediicalStaffPermissionRepository = mediicalStaffPermissionRepository;
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            var validator = new AddMedicalStaffValidator();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            bool isMedicalStaffExist = await _medicalStaffrepository.CheckExistByOfficeIdAndPhoneNumber(
                request.DTO.OfficeId, request.DTO.PhoneNumber);

            if (isMedicalStaffExist)
            {
                var error = $"There is a medical staff with this phoneNumber in this office";
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);

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

                    var role = _roleManager.FindByNameAsync("PATIENT").Result;
                    if (role != null)
                    {
                        await _userOfficeRoleRepository.Add(new UserOfficeRole
                        {
                            RoleId = role.Id,
                            UserId = user.Id,
                        });

                        await _userManager.AddToRoleAsync(user, role.NormalizedName);
                    }
                }
                else
                {
                    var error = $"There is a problem in registering user";
                    return await Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error);
                }
            }

            var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            medicalStaff.UserId = user.Id;
            medicalStaff = await _medicalStaffrepository.Add(medicalStaff);

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
                            OfficeId = request.DTO.OfficeId
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

            return await Success(HttpStatusCode.Created, $"{_requestTitle} succeded", medicalStaff.Id);
        }

        private async Task<BaseResponse> Success(HttpStatusCode statusCode, string message, params object[] data)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = message,
                AdditionalData = data
            });
            return new() { StatusCode = statusCode, Success = true, StatusDescription = message, Data = data.ToList() };
        }

        private async Task<BaseResponse> Faild(HttpStatusCode statusCode, string message, params string[] errors)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = message,
                AdditionalData = errors
            });
            return new() { StatusCode = statusCode, Success = false, StatusDescription = message, Errors = errors.ToList() };
        }
    }
}

