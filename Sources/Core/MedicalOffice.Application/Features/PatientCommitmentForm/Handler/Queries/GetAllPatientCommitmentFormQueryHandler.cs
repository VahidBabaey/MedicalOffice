using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using MedicalOffice.Application.Models;
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

    public class GetAllPatientCommitmentFormQueryHandler : IRequestHandler<GetAllPatientCommitmentsFormQuery, BaseResponse>
    {
        private readonly IPatientCommitmentFormRepository _patientcommitmentformrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientCommitmentFormQueryHandler(IPatientCommitmentFormRepository patientcommitmentformrepository, IMapper mapper, ILogger logger)
        {
            _patientcommitmentformrepository = patientcommitmentformrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllPatientCommitmentsFormQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var patientCommitmentsForms = await _patientcommitmentformrepository.GetByPatientId(request.PatientId);
                var result = _mapper.Map<List<PatientCommitmentsFormListDTO>>(patientCommitmentsForms.Skip(request.DTO.Skip).Take(request.DTO.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = patientCommitmentsForms.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = patientCommitmentsForms.Count(), result = result });
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
