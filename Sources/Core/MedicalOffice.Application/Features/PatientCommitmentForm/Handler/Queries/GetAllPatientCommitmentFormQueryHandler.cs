using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query
{

    public class GetAllPatientCommitmentFormQueryHandler : IRequestHandler<GetAllPatientCommitmentsFormQuery, List<PatientCommitmentsFormListDTO>>
    {
        private readonly IPatientCommitmentFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientCommitmentFormQueryHandler(IPatientCommitmentFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<PatientCommitmentsFormListDTO>> Handle(GetAllPatientCommitmentsFormQuery request, CancellationToken cancellationToken)
        {
            List<PatientCommitmentsFormListDTO> result = new();

            Log log = new();

            try
            {
                var patientCommitmentsForms = await _repository.GetByPatientId(request.PatientId);

                result = _mapper.Map<List<PatientCommitmentsFormListDTO>>(patientCommitmentsForms);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.Messages.Add(error.Message);
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
