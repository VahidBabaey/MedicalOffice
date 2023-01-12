using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Handlers.Queries
{

    public class GetAllExperimentQueryHandler : IRequestHandler<GetAllExperimentQuery, List<ExperimentListDTO>>
    {
        private readonly IExperimentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllExperimentQueryHandler(IExperimentRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<ExperimentListDTO>> Handle(GetAllExperimentQuery request, CancellationToken cancellationToken)
        {
            List<ExperimentListDTO> result = new();

            Log log = new();

            try
            {
                var experiments = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

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
