using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, BaseResponse>
    {
        private readonly IValidator<ResetPasswordDTO> _validator;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManagr;
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
            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
                return await Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());

            var user = await _userManagr.FindByNameAsync(request.Dto.PhoneNumber);

            if (user == null)
            {
                var error = $"This user didn't registered yet";
                return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var changePassword = await _userManagr.ChangePasswordAsync(user, request.Dto.CurrentPassword, request.Dto.NewPassword);

            if (!changePassword.Succeeded)
            {
                return await Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed");
            }

            return await Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", user.Id);
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

