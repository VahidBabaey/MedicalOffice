using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query
{

    public class GetAlliillnessReasonsQueryHandler : IRequestHandler<GetAlliillnessReasonsQuery, List<BasicInfoDetailListDTO>>
    {
        private readonly IPatientIllnessFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAlliillnessReasonsQueryHandler(IPatientIllnessFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<BasicInfoDetailListDTO>> Handle(GetAlliillnessReasonsQuery request, CancellationToken cancellationToken)
        {
            List<BasicInfoDetailListDTO> result = new();

            Log log = new();

            try
            {
                var illnessreasons = await _repository.GetByBasicInfoId();

                result = _mapper.Map<List<BasicInfoDetailListDTO>>(illnessreasons);

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
