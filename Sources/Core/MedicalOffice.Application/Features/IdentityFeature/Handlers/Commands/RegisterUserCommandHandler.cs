using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Identity.Validators;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;
        public RegisterUserCommandHandler(
            IUserOfficeRoleRepository userOfficeRoleRepository,
            ILogger logger,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserValidator();
            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", validationResult.Errors.Select(error => error.ErrorMessage).ToArray());

            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p =>
                p.PhoneNumber == request.DTO.PhoneNumber ||
                p.NationalID == request.DTO.NationalID);

            if (existingUser != null)
            {
                var error = $"PhoneNumber: '{request.DTO.PhoneNumber}' or nationalId: '{request.DTO.NationalID}' already exists.";
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            try
            {

                var user = _mapper.Map<User>(request.DTO);
                user.Id = Guid.NewGuid();
                user.UserName = request.DTO.PhoneNumber;

                var userCreation = await _userManager.CreateAsync(user);

                //MakeSureUserIsCreatedOrThrowException();
                if (!userCreation.Succeeded)
                {
                    var errors = userCreation.Errors.Select(x => $"{x.Code} - {x.Description}").ToArray();
                    return await Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", errors);
                }

                var userOfficeRoles = new List<UserOfficeRole>();
                var roleName = new List<string>();
                //TODO: check current user
                if (request.DTO.RoleIds != null)
                {
                    //MakeSureUserIsSuperAdminOrThrowException();
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

                            roleName.Add(role.NormalizedName);
                        }
                    }
                    await _userManager.AddToRolesAsync(user, roleName);
                    await _userOfficeRoleRepository.AddUserOfficeRoles(userOfficeRoles);
                }

                var patientRole = _roleManager.FindByNameAsync("PATIENT").Result;

                //MakeSurePatientRoleExistsOrThrowException();
                if (patientRole == null)
                {
                    const string error = $"there is no suitable patientRole for assigning to user";

                    return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
                }

                await _userOfficeRoleRepository.InsertToUserOfficeRole(
                    UserId: user.Id,
                    roleId: patientRole.Id
               );

                await _userManager.AddToRoleAsync(user, patientRole.NormalizedName);

                return await Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { user.Id });
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
