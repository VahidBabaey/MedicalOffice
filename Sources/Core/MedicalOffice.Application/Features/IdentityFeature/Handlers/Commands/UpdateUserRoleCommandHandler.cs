using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
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

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, BaseResponse>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IOfficeRepository _officeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserOfficeRoleRepository _usercOfficeRoleRepository;
        private readonly IValidator<UserRoleDTO> _validator;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public UpdateUserRoleCommandHandler(
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IOfficeRepository officeRepository,
            IValidator<UserRoleDTO> validator,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            ILogger logger
            )
        {
            _officeRepository = officeRepository;
            _roleManager = roleManager;
            _usercOfficeRoleRepository = userOfficeRoleRepository;
            _userManager = userManager;
            _logger = logger;
            _validator = validator;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            var user = await _userManager.FindByNameAsync(request.Dto.PhoneNumber);
            if (user == null)
            {
                var error = "User not found";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed", error);
            }

            var office = _officeRepository.Get(request.Dto.OfficeId);
            if (office == null)
            {
                var error = "Office not found";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed", error);
            }

            var role = await _roleManager.FindByIdAsync(request.Dto.RoleId.ToString());
            if (role == null)
            {
                var error = "Role not found";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed", error);
            }

            var IsuserOfficeRoleExist = _usercOfficeRoleRepository
                .GetAll().Result
                .Any(uor =>
                    uor.UserId == user.Id &&
                    uor.OfficeId == request.Dto.OfficeId &&
                    uor.RoleId == request.Dto.RoleId);
            if (IsuserOfficeRoleExist)
            {
                var error = "This UserOfficeRole is Exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.Conflict,
                    $"{_requestTitle} failed", error);
            }

            var updateUserRoles = await _userManager.AddToRoleAsync(user, role.NormalizedName);
            if (!updateUserRoles.Succeeded)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = updateUserRoles.Errors.Select(error => error.ToString()).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    updateUserRoles.Errors.Select(error => error.ToString()).ToArray());
            }

            try
            {
                var updateUserOfficeRole = await _usercOfficeRoleRepository.Add(new UserOfficeRole
                {
                    UserId = user.Id,
                    RoleId = request.Dto.RoleId,
                    OfficeId = request.Dto.OfficeId
                });

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = updateUserOfficeRole
                });

                return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} failed", updateUserOfficeRole);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}