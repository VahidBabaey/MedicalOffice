using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Handlers.Command
{
    public class AddServiceRoomCommandHandler : IRequestHandler<AddServiceRoomCommand, BaseResponse>
    {
        private readonly IValidator<AddServiceRoomDTO> _validator;
        private readonly IRoomRepository _roomRepository;
        private readonly IServiceRoomRepository _serviceRoomRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;

        public AddServiceRoomCommandHandler(
            IValidator<AddServiceRoomDTO> validator,
            IRoomRepository RoomRepository,
            IServiceRoomRepository serviceRoomRepository,
            ILogger logger,
            IMapper mapper)
        {
            _validator = validator;
            _roomRepository = RoomRepository;
            _serviceRoomRepository = serviceRoomRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddServiceRoomCommand request, CancellationToken cancellationToken)
        {
            #region Validate
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }
            #endregion

            var room = _mapper.Map<Room>(request.DTO);
            room.OfficeId = request.OfficeId;
            var result = await _roomRepository.Add(room);

            var serviceRooms = new List<ServiceRoom>();
            foreach (var item in request.DTO.ServiceIds)
            {
                serviceRooms.Add(new ServiceRoom
                {
                    RoomId = room.Id,
                    ServiceId = item
                }); 
            }

            await _serviceRoomRepository.AddRange(serviceRooms);    

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result.Id);
        }
    }
}
