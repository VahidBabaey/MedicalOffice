using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
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
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly string _requestTitle;

        public AddMedicalStaffCommandHandler(
            IMapper mapper,
            ILogger logger,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IMedicalStaffRepository medicalStaffrepository,
            IUserOfficeRoleRepository userOfficeRoleRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffrepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            AddMedicalStaffValidator validator = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            bool isMedicalStaffExist = await _medicalStaffrepository.GetByOfficeIdAndPhoneNumber(
                request.DTO.OfficeId, request.DTO.PhoneNumber);

            if (isMedicalStaffExist)
            {
                var error = $"There is a medical staff with this phoneNumber in this office";
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
            if (user == null)
            {
                user = _mapper.Map<User>(request.DTO);
                user.UserName = request.DTO.PhoneNumber;
                user.NormalizedUserName = request.DTO.PhoneNumber;

                var userCreation = await _userManager.CreateAsync(user);
                if (userCreation.Succeeded)
                    user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                else
                {
                    var error = $"There is a problem in registering user";
                    return await Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error);
                }
            }

            try
            {
                var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
                medicalStaff.UserId = user.Id;
                medicalStaff = await _medicalStaffrepository.Add(medicalStaff);

                if (request.DTO.RoleIds != null)
                {
                    foreach (var roleId in request.DTO.RoleIds)
                    {
                        Role role = await _roleManager.FindByIdAsync(roleId.ToString());
                        if (role != null)
                        {
                            await _userOfficeRoleRepository.InsertToUserOfficeRole(roleId, user.Id, request.DTO.OfficeId);

                            await _userManager.AddToRoleAsync(user, role.NormalizedName);
                        }
                    }
                }

                var patientRole = _roleManager.FindByNameAsync("PATIENT").Result;
                if (patientRole != null)
                {
                    await _userOfficeRoleRepository.InsertToUserOfficeRole(
                    UserId: user.Id,
                    roleId: patientRole.Id);
                }

                return await Success(HttpStatusCode.Created, $"{_requestTitle} succeded", (new { Id = medicalStaff.Id }));
            }
            catch (Exception error)
            {
                return await Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error.Message);
            }
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

