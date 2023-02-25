using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Handlers.Commands
{

    public class DeletePatientReferralFormCommandHandler : IRequestHandler<DeletePatientReferralFormCommand, BaseResponse>
    {
        private readonly IPatientReferralFormRepository _patientreferralformrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeletePatientReferralFormCommandHandler(IPatientReferralFormRepository patientreferralformrepository, ILogger logger)
        {
            _patientreferralformrepository = patientreferralformrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeletePatientReferralFormCommand request, CancellationToken cancellationToken)
        {

            var validationPatientReferralFormId = await _patientreferralformrepository.CheckExistPatientReferralFormId(request.PatientReferralFormId);

            if (!validationPatientReferralFormId)
            {
                var error = "ID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            try
            {
                await _patientreferralformrepository.SoftDelete(request.PatientReferralFormId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
