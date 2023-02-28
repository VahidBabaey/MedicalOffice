using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Handlers.Command
{
    public class EditServiceRoomCommandHandler : IRequestHandler<EditServiceRoomCommand, BaseResponse>
    {
        private readonly IValidator<UpdateServiceRoomDTO> _validator;
        private readonly IServiceRoomRepository _serviceRoomRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;

        public EditServiceRoomCommandHandler(
            IValidator<UpdateServiceRoomDTO> validator,
            IServiceRoomRepository serviceRoomRepository,
            IRoomRepository RoomRepository,
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

        public async Task<BaseResponse> Handle(EditServiceRoomCommand request, CancellationToken cancellationToken)
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

            #region UpdateServiceRoom
            var room = _mapper.Map<Room>(request.DTO);
            room.OfficeId = request.OfficeId;
            #endregion

            #region CreateNewServiceRooms
            var serviceRooms = new List<ServiceRoom>();
            foreach (var item in request.DTO.ServiceIds)
            {
                serviceRooms.Add(new ServiceRoom
                {
                    RoomId = room.Id,
                    ServiceId = item,
                });
            }
            #endregion

            #region UpdateRoomAndService
            var result = await _roomRepository.UpdateRoomAndRoomServices(room, serviceRooms);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
            #endregion
        }
    }
}
