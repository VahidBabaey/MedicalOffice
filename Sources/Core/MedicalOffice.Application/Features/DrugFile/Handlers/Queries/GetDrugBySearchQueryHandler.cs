using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Handlers.Queries
{

    public class GetDrugBySearchQueryHandler : IRequestHandler<GetDrugBySearchQuery, List<DrugListDTO>>
    {
        private readonly IDrugRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetDrugBySearchQueryHandler(IDrugRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<DrugListDTO>> Handle(GetDrugBySearchQuery request, CancellationToken cancellationToken)
        {
            List<DrugListDTO> result = new();

            Log log = new();

            try
            {
                var drug = await _repository.GetDrugBySearch(request.Name);

                result = _mapper.Map<List<DrugListDTO>>(drug.Where(p => p.OfficeId == request.OfficeId));

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
