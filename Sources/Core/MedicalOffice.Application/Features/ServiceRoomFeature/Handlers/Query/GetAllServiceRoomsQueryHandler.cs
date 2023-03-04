using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Query;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Handlers.Query
{
    public class GetAllServiceRoomsQueryHandler : IRequestHandler<GetAllServiceRoomsQuery, BaseResponse>
    {
        private readonly IRoomRepository _serviceRoomRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;

        public GetAllServiceRoomsQueryHandler(
            IRoomRepository serviceRoomRepository,
            ILogger logger,
            IMapper mapper)
        {
            _serviceRoomRepository = serviceRoomRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllServiceRoomsQuery request, CancellationToken cancellationToken)
        {
            var serviceRooms = await _serviceRoomRepository.GetServiceRooms(request.OfficeId);

            var result = serviceRooms.Skip(request.DTO.Skip).Take(request.DTO.Take);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = serviceRooms.Count, result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = serviceRooms.Count, result = result });
        }
    }
}