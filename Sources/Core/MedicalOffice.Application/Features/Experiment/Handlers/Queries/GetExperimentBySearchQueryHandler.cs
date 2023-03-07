using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Handlers.Queries
{

    public class GetExperimentBySearchQueryHandler : IRequestHandler<GetExperimentBySearchQuery, BaseResponse>
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

        public async Task<BaseResponse> Handle(GetExperimentBySearchQuery request, CancellationToken cancellationToken)
        {
            Log log = new();

            try
            {
                var experiments = await _repository.GetExperimentBySearch(request.Name);
                var result = _mapper.Map<List<ExperimentListDTO>>(experiments.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false).Take(request.Dto.Take).Skip(request.Dto.Skip));

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
                log.AdditionalData = result;
                await _logger.Log(log);

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = experiments.Count(), result = result });
            }

            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData = error.Message;
                log.Type = LogType.Error;
                await _logger.Log(log);

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
