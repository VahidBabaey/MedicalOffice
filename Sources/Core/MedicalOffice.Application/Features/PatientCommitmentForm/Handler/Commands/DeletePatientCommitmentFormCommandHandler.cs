using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Command
{

    public class DeletePatientCommitmentFormCommandHandler : IRequestHandler<DeletePatientCommitmentFormCommand, BaseResponse>
    {
        private readonly IPatientCommitmentFormRepository _patientcommitmentformrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeletePatientCommitmentFormCommandHandler(IPatientCommitmentFormRepository patientcommitmentformrepository, ILogger logger)
        {
            _patientcommitmentformrepository = patientcommitmentformrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeletePatientCommitmentFormCommand request, CancellationToken cancellationToken)
        {

            var validationPatientCommitmentFormId = await _patientcommitmentformrepository.CheckExistPatientCommitmentFormId(request.PatientCommitmentFormId);

            if (!validationPatientCommitmentFormId)
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
                await _patientcommitmentformrepository.SoftDelete(request.PatientCommitmentFormId);

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
