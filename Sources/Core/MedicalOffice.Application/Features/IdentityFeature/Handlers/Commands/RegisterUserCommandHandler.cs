using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Identity;
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
        private readonly IValidator<RegisterUserDTO> _validator;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;
        public RegisterUserCommandHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IValidator<RegisterUserDTO> validator,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            ILogger logger,
            IMapper mapper
            )
        {
            _validator = validator;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                }); 

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", 
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p =>
                p.PhoneNumber == request.DTO.PhoneNumber ||
                p.NationalID == request.DTO.NationalID);

            if (existingUser != null)
            {
                var error = $"PhoneNumber: '{request.DTO.PhoneNumber}' or nationalId: '{request.DTO.NationalID}' already exists.";

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
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
                    var error = userCreation.Errors.Select(x => $"{x.Code} - {x.Description}").ToArray();

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error);
                }

                var userOfficeRoles = new List<UserOfficeRole>();
                var roleName = new List<string>();

                var patientRole = _roleManager.FindByNameAsync("PATIENT").Result;

                //MakeSurePatientRoleExistsOrThrowException();
                if (patientRole == null)
                {
                    const string error = $"there is no suitable patientRole for assigning to user";

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
                }

                await _userOfficeRoleRepository.InsertToUserOfficeRole(
                    UserId: user.Id,
                    roleId: patientRole.Id
               );

                await _userManager.AddToRoleAsync(user, patientRole.NormalizedName);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { user.Id }
                });

                return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { user.Id });
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
