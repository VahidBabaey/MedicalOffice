using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query
{

    public class GetAllPatientIllnessFormQueryHandler : IRequestHandler<GetAllPatientIllnessFormQuery, BaseResponse>
    {
        private readonly IPatientIllnessFormRepository _patientillnessformrepository;
        private readonly IPatientRepository _patientrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientIllnessFormQueryHandler(IPatientRepository patientrepository, IPatientIllnessFormRepository patientillnessformrepository, IMapper mapper, ILogger logger)
        {
            _patientrepository = patientrepository;
            _patientillnessformrepository = patientillnessformrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllPatientIllnessFormQuery request, CancellationToken cancellationToken)
        {

            var validationPatientId = await _patientrepository.CheckExistPatientId(request.OfficeId, request.PatientId);

            if (!validationPatientId)
            {
                var error = "PatientID isn't exist";
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
                var patientillnessforms = await _patientillnessformrepository.GetByPatientId(request.PatientId);
                var result = _mapper.Map<List<PatientIllnessFormListDTO>>(patientillnessforms.Skip(request.DTO.Skip).Take(request.DTO.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = patientillnessforms.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = patientillnessforms.Count(), result = result });
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
