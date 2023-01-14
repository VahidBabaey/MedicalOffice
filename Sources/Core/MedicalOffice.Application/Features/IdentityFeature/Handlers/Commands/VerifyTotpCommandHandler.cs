using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class VerifyTotpCommandHandler : IRequestHandler<VerifyTotpCommand, BaseResponse>
    {
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public VerifyTotpCommandHandler(ITotpHandler totpHandler, ILogger logger)
        {
            _totpHandler = totpHandler;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(VerifyTotpCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var isVerify = _totpHandler.Verify(request.DTO.PhoneNumber, request.DTO.Totp);
            if (!isVerify)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = false
                });

                return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} failed", false);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = true
            });

            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", true);
        }
    }
}
