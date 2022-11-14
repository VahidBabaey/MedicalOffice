using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Handlers.Queries
{

    public class GetAllAdditionalInsuranceNamesQueryHandler : IRequestHandler<GetAllAdditionalInsuranceNamesQuery, List<InsuranceNamesDTO>>
    {
        private readonly IInsuranceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllAdditionalInsuranceNamesQueryHandler(IInsuranceRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<InsuranceNamesDTO>> Handle(GetAllAdditionalInsuranceNamesQuery request, CancellationToken cancellationToken)
        {
            List<InsuranceNamesDTO> result = new();

            Log log = new();

            try
            {
                var insuranceNames = await _repository.GetAllAdditionalInsuranceNames();

                result = _mapper.Map<List<InsuranceNamesDTO>>(insuranceNames);

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
