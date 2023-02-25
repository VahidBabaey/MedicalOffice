using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Command
{

    public class DeletePatientIllnessFormCommandHandler : IRequestHandler<DeletePatientIllnessFormCommand, BaseResponse>
    {
        private readonly IPatientIllnessFormRepository _patientillnessformrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeletePatientIllnessFormCommandHandler(IPatientIllnessFormRepository patientillnessformrepository, ILogger logger)
        {
            _patientillnessformrepository = patientillnessformrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeletePatientIllnessFormCommand request, CancellationToken cancellationToken)
        {

            var validationPatientIllnessFormId = await _patientillnessformrepository.CheckExistPatientIllnessFormId(request.PatientIllnessFormId);

            if (!validationPatientIllnessFormId)
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
                await _patientillnessformrepository.SoftDelete(request.PatientIllnessFormId);

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
