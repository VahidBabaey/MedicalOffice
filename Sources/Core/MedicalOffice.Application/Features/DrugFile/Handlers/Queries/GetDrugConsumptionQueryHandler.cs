using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Handlers.Queries
{

    public class GetDrugConsumptionQueryHandler : IRequestHandler<GetDrugConsumptionQuery, List<DrugConsumptionListDTO>>
    {
        private readonly IDrugConsumptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetDrugConsumptionQueryHandler(IDrugConsumptionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;       
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<DrugConsumptionListDTO>> Handle(GetDrugConsumptionQuery request, CancellationToken cancellationToken)
        {
            List<DrugConsumptionListDTO> result = new();

            Log log = new();

            try
            {
                var drugconsumption = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

                result = _mapper.Map<List<DrugConsumptionListDTO>>(drugconsumption);

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
