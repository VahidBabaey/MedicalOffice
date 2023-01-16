using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Handlers.Queries
{

    public class GetExperimentBySearchQueryHandler : IRequestHandler<GetExperimentBySearchQuery, List<ExperimentListDTO>>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IExperimentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetExperimentBySearchQueryHandler(IOfficeRepository officeRepository, IExperimentRepository repository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<ExperimentListDTO>> Handle(GetExperimentBySearchQuery request, CancellationToken cancellationToken)
        {
            List<ExperimentListDTO> result = new();

            BaseResponse response = new();

            Log log = new();

            try
            {
                var experiments = await _repository.GetExperimentBySearch(request.Name);

                result = _mapper.Map<List<ExperimentListDTO>>(experiments.Where(p => p.OfficeId == request.OfficeId));

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData = error.Message;
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
