using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, BaseResponse>
    {
        private readonly IValidator<SetPasswordDTO> _validator;
        readonly ILogger _logger;
        readonly UserManager<User> _userManagr;
        readonly string _requestTitle;

        public SetPasswordCommandHandler(
            IValidator<SetPasswordDTO> validator,
            ILogger logger,
            UserManager<User> userManagr)
        {
            _validator = validator;
            _logger = logger;
            _userManagr = userManagr;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var user = await _userManagr.FindByNameAsync(request.DTO.PhoneNumber);
            if (user == null)
            {
                var error = "The user didn't registered yet";
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var changePassword = await _userManagr.AddPasswordAsync(user, request.DTO.Password);
            if (!changePassword.Succeeded)
            {
                var error = changePassword.Errors.Select(error => error.Description).ToArray();
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = user.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", user.Id);
        }
    }
}