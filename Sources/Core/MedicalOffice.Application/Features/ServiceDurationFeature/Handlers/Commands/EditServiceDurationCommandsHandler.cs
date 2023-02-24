using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.ServiceDurationFeature.Requests.Commands;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Handlers.Commands
{
    public class EditServiceDurationCommandsHandler : IRequestHandler<EditServiceDurationCommand, BaseResponse>
    {
        private readonly IValidator<EditServiceDurationDTO> _validator;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public EditServiceDurationCommandsHandler(
            IValidator<EditServiceDurationDTO> validator,
            ILogger logger,
            IServiceDurationRepositopry serviceDurationRepository)
        {
            _validator = validator;
            _logger = logger;
            _serviceDurationRepository = serviceDurationRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditServiceDurationCommand request, CancellationToken cancellationToken)
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

            #region checkStaffServiceExist
            var existingServiceDuration = await _serviceDurationRepository.GetById(request.DTO.Id);
            if (existingServiceDuration == null || existingServiceDuration.OfficeId != request.OfficeId)
            {
                var error = "This service duration isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error
                });

                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }
            #endregion

            #region UpdateServiceDuration            
            existingServiceDuration.ServiceId = request.DTO.ServiceId;
            existingServiceDuration.MedicalStaffId = request.DTO.MedicalStaffId;
            existingServiceDuration.Duration = request.DTO.Duration;

            await _serviceDurationRepository.Update(existingServiceDuration);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = existingServiceDuration.Id
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", existingServiceDuration.Id);
            #endregion
        }
    }
}
