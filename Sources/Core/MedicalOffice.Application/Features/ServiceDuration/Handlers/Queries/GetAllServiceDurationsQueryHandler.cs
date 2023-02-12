using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Handlers.Queries
{
    public class GetAllServiceDurationsQueryHandler : IRequestHandler<GetAllServiceDurationQuery, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mappper;
        private readonly IServiceDurationRepositopry _ServiceDurationRepository;

        private readonly string _requestTitle;

        public GetAllServiceDurationsQueryHandler(ILogger logger, IMapper mappper, IServiceDurationRepositopry serviceDurationRepository)
        {
            _logger = logger;
            _mappper = mappper;
            _ServiceDurationRepository = serviceDurationRepository;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllServiceDurationQuery request, CancellationToken cancellationToken)
        {
            

            try
            {
                var allServices = await _ServiceDurationRepository.GetAllWithPagination(request.DTO.Skip, request.DTO.Take);

                var result = _mappper.Map<List<ServiceDurationListDTO>>(allServices);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                });

                return ResponseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    result);
            }

            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return ResponseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}
