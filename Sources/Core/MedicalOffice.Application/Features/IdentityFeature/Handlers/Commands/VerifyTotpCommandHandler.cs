using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class VerifyTotpCommandHandler : IRequestHandler<VerifyTotpCommand, BaseResponse>
    {
        private readonly IValidator<VerifyTotpDTO> _validator;
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public VerifyTotpCommandHandler(IValidator<VerifyTotpDTO> validator, ITotpHandler totpHandler, ILogger logger)
        {
            _validator = validator;
            _totpHandler = totpHandler;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(VerifyTotpCommand request, CancellationToken cancellationToken)
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

            var isVerify = _totpHandler.Verify(request.DTO.PhoneNumber, request.DTO.Totp);
            if (!isVerify)
            {
                var error = "Totp isn't valid";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return ResponseBuilder.Faild(HttpStatusCode.NotAcceptable, $"{_requestTitle} failed", error);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = true
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", true);
        }
    }
}