﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Models.Logger;
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

        public GetAllServiceDurationsQueryHandler(
            ILogger logger, 
            IMapper mappper, 
            IServiceDurationRepositopry serviceDurationRepository)
        {
            _logger = logger;
            _mappper = mappper;
            _ServiceDurationRepository = serviceDurationRepository;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllServiceDurationQuery request, CancellationToken cancellationToken)
        {
            var services = await _ServiceDurationRepository.GetAllServiceDurations(request.OfficeId);
            var result = services.Skip(request.DTO.Skip).Take(request.DTO.Take);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = new { total = services.Count, result = result }
            });

            return ResponseBuilder.Success(HttpStatusCode.OK,
                $"{_requestTitle} succeeded",
                new { total = services.Count, result = result });
        }
    }
}
