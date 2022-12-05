using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Handlers.Queries
{

    public class GetAllInsuranceQueryHandler : IRequestHandler<GetAllInsuranceQuery, List<InsuranceListDTO>>
    {
        private readonly IInsuranceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllInsuranceQueryHandler(IInsuranceRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<InsuranceListDTO>> Handle(GetAllInsuranceQuery request, CancellationToken cancellationToken)
        {
            List<InsuranceListDTO> result = new();

            Log log = new();

            try
            {
                var insurances = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

                result = _mapper.Map<List<InsuranceListDTO>>(insurances);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                ;
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
