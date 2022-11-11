using AutoMapper;
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
    public class SendTotpCommandHandler : IRequestHandler<SendTotpCommand, BaseCommandResponse>
    {
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public SendTotpCommandHandler(ITotpHandler totpHandler, ILogger logger)
        {
            _totpHandler = totpHandler;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseCommandResponse> Handle(SendTotpCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            PhoneNumberValidator validator = new();
            Log log = new Log();

            try
            {
                var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);
                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Message = $"{_requestTitle} failed";
                    response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                    log.Type = LogType.Error;
                }

                var totp = _totpHandler.Generate(request.DTO.PhoneNumber);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Message = $"Totp '{totp}' was sent to user" });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.Message;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
