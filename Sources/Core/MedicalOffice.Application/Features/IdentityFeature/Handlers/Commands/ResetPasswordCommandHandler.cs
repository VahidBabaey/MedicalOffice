using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Exceptions;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponse>
    {
        private readonly IValidator<ResetPasswordDTO> _validator;
        private readonly UserManager<User> _userManagr;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public ResetPasswordCommandHandler(
            IValidator<ResetPasswordDTO> validator,
            ILogger logger,
            UserManager<User> userManagr)
        {
            _validator = validator;
            _logger = logger;
            _userManagr = userManagr;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
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

            var changePassword = await _userManagr.ChangePasswordAsync(user, request.DTO.CurrentPassword, request.DTO.NewPassword);

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
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succedded", user.Id);
        }
    }
}

