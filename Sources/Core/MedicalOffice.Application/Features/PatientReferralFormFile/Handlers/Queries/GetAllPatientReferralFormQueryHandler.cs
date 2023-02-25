using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
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

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Handlers.Queries
{

    public class GetAllPatientReferralFormQueryHandler : IRequestHandler<GetAllPatientReferralFormQuery, BaseResponse>
    {
        private readonly IPatientRepository _patientrepository;
        private readonly IPatientReferralFormRepository _patientreferralformrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientReferralFormQueryHandler(IPatientRepository patientrepository, IPatientReferralFormRepository patientreferralformrepository, IMapper mapper, ILogger logger)
        {
            _patientrepository = patientrepository;
            _patientreferralformrepository = patientreferralformrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllPatientReferralFormQuery request, CancellationToken cancellationToken)
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
                var patientreferralforms = await _patientreferralformrepository.GetByPatientId(request.PatientId);
                var result = _mapper.Map<List<PatientReferralFormListDTO>>(patientreferralforms.Skip(request.DTO.Skip).Take(request.DTO.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = patientreferralforms.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = patientreferralforms.Count(), result = result });
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
