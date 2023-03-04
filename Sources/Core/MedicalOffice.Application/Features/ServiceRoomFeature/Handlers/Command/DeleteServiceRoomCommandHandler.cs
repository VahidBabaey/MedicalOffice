using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command;
using MedicalOffice.Application.Models.Logger;
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
    public class DeleteServiceRoomCommandHandler : IRequestHandler<DeleteServiceRoomCommand, BaseResponse>
    {
        private readonly IValidator<ServiceRoomIdsDTO> _validator;
        private readonly IRoomRepository _serviceRoomRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;

        public DeleteServiceRoomCommandHandler(
            IValidator<ServiceRoomIdsDTO> validator,
            IRoomRepository serviceRoomRepository,
            ILogger logger,
            IMapper mapper)
        {
            _validator = validator;
            _serviceRoomRepository = serviceRoomRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteServiceRoomCommand request, CancellationToken cancellationToken)
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

            var rooms = await _serviceRoomRepository.SoftDeleteRange(request.DTO.RoomIds);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = rooms
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", rooms);
            #endregion
        }
    }
}
