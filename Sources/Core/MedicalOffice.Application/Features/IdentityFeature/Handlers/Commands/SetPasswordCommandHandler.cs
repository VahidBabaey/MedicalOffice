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

        public SetPasswordCommandHandler(ILogger logger, UserManager<User> userManagr)
        {
            _logger = logger;
            _userManagr = userManagr;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManagr.FindByNameAsync(request.DTO.PhoneNumber);

            if (user == null)
            {
                var error = $"This user didn't registered yet";
                return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var changePassword = await _userManagr.AddPasswordAsync(user, request.DTO.Password);

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
