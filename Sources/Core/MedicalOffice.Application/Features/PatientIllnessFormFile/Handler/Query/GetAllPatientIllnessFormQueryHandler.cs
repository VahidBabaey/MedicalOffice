using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query
{

    public class GetAllPatientIllnessFormQueryHandler : IRequestHandler<GetAllPatientIllnessFormQuery, List<PatientIllnessFormListDTO>>
    {
        private readonly IPatientIllnessFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllPatientIllnessFormQueryHandler(IPatientIllnessFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<PatientIllnessFormListDTO>> Handle(GetAllPatientIllnessFormQuery request, CancellationToken cancellationToken)
        {
            List<PatientIllnessFormListDTO> result = new();

            Log log = new();

            try
            {
                var patientillnessforms = await _repository.GetByPatientId(request.PatientId);

                result = _mapper.Map<List<PatientIllnessFormListDTO>>(patientillnessforms);

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
