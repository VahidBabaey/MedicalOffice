using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class SendTotpCommandHandler : IRequestHandler<SendTotpCommand, BaseResponse>
    {
        private readonly IValidator<PhoneNumberDTO> _validator;
        private readonly ISmsSender _smsSender;
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public SendTotpCommandHandler(
            IValidator<PhoneNumberDTO> validator,
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
            BaseResponse response = new();
            Log log = new Log();

            try
            {
                var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                    log.Type = LogType.Error;
                }

                var totp = _totpHandler.Generate(request.Dto.PhoneNumber);

                //TODO: add sms provider to send sms to user.
                //var totpSms = new TotpSms()
                //{
                //    Type = 1,
                //    Receptor = new string[] { request.Dto.PhoneNumber },
                //    Code = totp
                //};

                //var sendMessageToUser = await _smsSender.SendTotpSmsAsync(totpSms);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Message = $"Totp '{totp}' was sent to user" });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
