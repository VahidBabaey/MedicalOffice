using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Handlers.Queries
{

    public class GetAllPatientReferralFormQueryHandler : IRequestHandler<GetAllPatientReferralFormQuery, List<PatientReferralFormListDTO>>
    {
        private readonly IPatientReferralFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientReferralFormQueryHandler(IPatientReferralFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<PatientReferralFormListDTO>> Handle(GetAllPatientReferralFormQuery request, CancellationToken cancellationToken)
        {
            List<PatientReferralFormListDTO> result = new();

            Log log = new();

            try
            {
                var patientreferralforms = await _repository.GetByPatientId(request.PatientId);

                result = _mapper.Map<List<PatientReferralFormListDTO>>(patientreferralforms);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData=error.Message;
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
