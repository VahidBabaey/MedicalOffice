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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseCommandResponse>
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

        public async Task<BaseCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserValidator();
            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
                return await Faild($"{_requestTitle} failed", validationResult.Errors.Select(error => error.ErrorMessage).ToArray());

            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p =>
                p.PhoneNumber == request.DTO.PhoneNumber ||
                p.NationalID == request.DTO.NationalID);

            if (existingUser != null)
            {
                var error = $"PhoneNumber: '{request.DTO.PhoneNumber}' or nationalId: '{request.DTO.NationalID}' already exists.";
                return await Faild($"{_requestTitle} failed", error);
            }

            var user = _mapper.Map<User>(request.DTO);
            user.Id = Guid.NewGuid();
            user.UserName = request.DTO.PhoneNumber;

            var userCreation = await _userManager.CreateAsync(user);

            //MakeSureUserIsCreatedOrThrowException();
            if (!userCreation.Succeeded)
            {
                var errors = userCreation.Errors.Select(x => $"{x.Code} - {x.Description}").ToArray();
                return await Faild($"{_requestTitle} failed", errors);
            }

            //TODO: check current user
            if (request.DTO.RoleIds != null)
            {
                //MakeSureUserIsSuperAdminOrThrowException();
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

            //MakeSurePatientRoleExistsOrThrowException();
            if (patientRole == null)
            {
                const string error = $"there is no suitable patientRole for assigning to user";

                return await Faild($"{_requestTitle} failed", error);
            }

            await _userOfficeRoleRepository.InsertToUserOfficeRole(
                UserId: user.Id,
                roleId: patientRole.Id
           );

            await _userManager.AddToRoleAsync(user, patientRole.NormalizedName);

            return await Success($"{_requestTitle} succeded", new { user.Id });
        }

        private async Task<BaseCommandResponse> Success(string message, params object[] data)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = message,
                AdditionalData = data
            });

            return new() { Success = true, Message = message, Data = data.ToList() };
        }

        private async Task<BaseCommandResponse> Faild(string message, params string[] errors)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = message,
                AdditionalData = errors
            });

            return new() { Success = false, Message = message, Errors = errors.ToList() };
        }
    }
}
