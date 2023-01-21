using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Handlers.Queries
{

    public class GetAllTariffByServiceIDQueryHandler : IRequestHandler<GetAllAdditionalInsuranceNamesQuery, BaseResponse>
    {
        private readonly IInsuranceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllTariffByServiceIDQueryHandler(IInsuranceRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllAdditionalInsuranceNamesQuery request, CancellationToken cancellationToken)
        {
            Log log = new();

            try
            {
                var insuranceNames = await _repository.GetAllAdditionalInsuranceNames();

                var result = _mapper.Map<List<InsuranceNamesDTO>>(insuranceNames);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
                log.AdditionalData = result;
                await _logger.Log(log);

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
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
