﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class UpdateAppointmentTypeCommandHandler : IRequestHandler<EditAppointmentTypeCommand, BaseResponse>
    {
        private readonly IValidator<UpdateAppointmentTypeDTO> _validator;
        private readonly ILogger _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly string _requestTitle;

        public UpdateAppointmentTypeCommandHandler(
            IValidator<UpdateAppointmentTypeDTO> validator,
            ILogger logger,
            IAppointmentRepository appointmentRepository)
        {
            _validator = validator;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(EditAppointmentTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var existingAppointment = _appointmentRepository.GetById(request.DTO.AppointmentId).Result;

            var validToChangeToFinalApproval = new AppointmentType[] { AppointmentType.Approved, AppointmentType.BetweenPatients };
            var validToChangeToCanceled = new AppointmentType[] { AppointmentType.Approved, AppointmentType.BetweenPatients, AppointmentType.FinalApproval };

            if (request.DTO.AppointmentType == AppointmentType.FinalApproval &&
                !validToChangeToFinalApproval.Contains(existingAppointment.AppointmentType))
            {
                var error = "Existing type doesn't have this capability to update to new type";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DTO.AppointmentType == AppointmentType.Canceled &&
                !validToChangeToCanceled.Contains(existingAppointment.AppointmentType))
            {
                var error = "Existing type doesn't have this capability to update to new type";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            existingAppointment.AppointmentType = request.DTO.AppointmentType;
            await _appointmentRepository.Update(existingAppointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",


                AdditionalData = existingAppointment.Id
            });

            return ResponseBuilder.Success(HttpStatusCode.BadRequest, $"{_requestTitle} succeeded", existingAppointment.Id);
        }
    }
}
