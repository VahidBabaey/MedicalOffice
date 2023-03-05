using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models.Logger;
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
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, BaseResponse>
    {
        private readonly UserManager<User> _userManagr;
        private readonly IValidator<SetPasswordDTO> _validator;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public ForgetPasswordCommandHandler(
            UserManager<User> userManagr,
            IValidator<SetPasswordDTO> validator,
            ILogger logger)
        {
            _validator = validator;
            _logger = logger;
            _userManagr = userManagr;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
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
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                        validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var user = await _userManagr.FindByNameAsync(request.DTO.PhoneNumber);

            if (user == null)
            {
                var error = $"This user didn't registered yet";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var token = await _userManagr.GeneratePasswordResetTokenAsync(user);

            var newPassword = await _userManagr.ResetPasswordAsync(user, token, request.DTO.Password);

            if (!newPassword.Succeeded)
            {
                var error = newPassword.Errors.Select(error => error.Description).ToArray();
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
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succedded", user.Id);
        }
    }
}
