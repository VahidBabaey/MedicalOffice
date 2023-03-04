﻿using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Models.Totp;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class SendTotpCommandHandler : IRequestHandler<SendTotpCommand, BaseResponse>
    {
        private readonly IValidator<SendTotpDTO> _validator;
        private readonly ISmsSender _smsSender;
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public SendTotpCommandHandler(
            IValidator<SendTotpDTO> validator,
            ISmsSender smsSender,
            ITotpHandler totpHandler,
            ILogger logger)
        {
            _validator = validator;
            _smsSender = smsSender;
            _totpHandler = totpHandler;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(SendTotpCommand request, CancellationToken cancellationToken)
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

            var totp = _totpHandler.Generate(request.DTO.PhoneNumber);

            //send sms to user.
            var totpSms = new TotpSms(1, new string[] { request.DTO.PhoneNumber }, totp);

            var sendMessageToUser = await _smsSender.SendTotpSmsAsync(totpSms);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = totp
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", totp);
        }
    }
}
