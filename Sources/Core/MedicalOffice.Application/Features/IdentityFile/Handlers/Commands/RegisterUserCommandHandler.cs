using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity.Validators;
using MedicalOffice.Application.Features.IdentityFile.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Application.Features.IdentityFile.Handlers.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseCommandResponse>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public RegisterUserCommandHandler(ILogger logger, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            BaseCommandResponse response = new();
            RegisterUserValidator validator = new();
            Log log = new();

            var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    var existingUser = await _userManager.Users.SingleOrDefaultAsync(p =>
                    p.PhoneNumber == request.Dto.PhoneNumber ||
                    p.NationalId == request.Dto.NationalId);

                    if (existingUser != null)
                    {
                        response.Success = false;
                        response.Message = $"{_requestTitle} failed";
                        response.Errors.Add($"PhoneNumber: '{request.Dto.PhoneNumber}' or nationalId: '{request.Dto.NationalId}' already exists.");

                        log.Type = LogType.Error;
                    }
                    else
                    {
                        var user = _mapper.Map<User>(request.Dto);
                        user.UserName = request.Dto.PhoneNumber;
                        user.LockoutEnabled = false;

                        var result = await _userManager.CreateAsync(user);
                        if (!result.Succeeded)
                        {
                            response.Success = false;
                            response.Message = $"{_requestTitle} failed";
                            response.Errors.Add(string.Join(",", result.Errors.Select(x => $"{x.Code} - {x.Description}")));

                            log.Type = LogType.Error;
                        }

                        //TODO: the role should come from role seed or from constant or env variable
                        var role = _roleManager.FindByNameAsync("Patient").Result;
                        if (role == null)
                        {
                            var patientRole = new Role
                            {
                                Id = Guid.NewGuid(),
                                Name = "Patient"
                            };

                            await _roleManager.CreateAsync(patientRole);
                        }

                        await _userManager.AddToRoleAsync(user, "Patient");

                        var createdUser = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.Dto.PhoneNumber);

                        response.Success = true;
                        response.Message = $"{_requestTitle} succeded";
                        response.Data.Add(new { Id = createdUser?.Id });

                        log.Type = LogType.Success;
                    }
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.Message = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
