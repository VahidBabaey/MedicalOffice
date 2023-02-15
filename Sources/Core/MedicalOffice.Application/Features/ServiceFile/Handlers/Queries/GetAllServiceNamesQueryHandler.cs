using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries
{
    public class GetAllServiceNamesQueryHandler : IRequestHandler<GetAllServiceNamesQuery, BaseResponse>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllServiceNamesQueryHandler(IServiceRepository serviceRepository,IMapper mapper,ILogger logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler",string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllServiceNamesQuery request, CancellationToken cancellationToken)
        {
            var services = _serviceRepository.GetAllByOfficeId(request.OfficeId).Result.Select(x=>_mapper.Map<ServiceIdNameDTO>(x)).ToList();
            
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = services
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = services.Count(), result = services });
        }
    }
}
