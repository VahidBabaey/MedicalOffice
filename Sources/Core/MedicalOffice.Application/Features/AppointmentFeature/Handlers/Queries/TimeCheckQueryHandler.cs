using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Helper;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class TimeCheckQueryHandler : IRequestHandler<TimeCheckQuery, BaseResponse>
    {
        private readonly IValidator<AddAppointmentDto> _validator;
        private readonly ILogger _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffScheduleRepository _medicalStaffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

        private readonly string _requestTitle;

        public TimeCheckQueryHandler(
            IValidator<AddAppointmentDto> validator,
            ILogger logger,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffScheduleRepository medicalStaffScheduleRepository,
            IServiceDurationRepositopry serviceDurationRepositopry
            )
        {
            _validator = validator;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _medicalStaffScheduleRepository = medicalStaffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepositopry;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(TimeCheckQuery request, CancellationToken cancellationToken)
        {
            
            var result = new CheckTimeResponseDTO();

            #region ValidateTime
            var isDateValid = _medicalStaffScheduleRepository.CheckTimeIsInStaffSchedule((Guid)request.DTO.MedicalStaffId, request.DTO.Date).Result;
            if (!isDateValid)
            {
                var error = new InvalidDataException("The staff is off in requested date");

                result.Message = "Staff is off in requested date";
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }
            #endregion

            var hasStaffService = _serviceDurationRepository.CheckStaffHasService(request.DTO.MedicalStaffId, request.DTO.ServiceId).Result;
            if (!hasStaffService)
            {
                result.Message = "The staff doesn't provide this service";
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }

            var serviceDuration = _serviceDurationRepository.GetByServiceAndStaffId(request.DTO.MedicalStaffId, request.DTO.ServiceId).Result;

            if (serviceDuration.Duration < (TimeOnly.Parse(request.EndTime) - TimeOnly.Parse(request.StartTime)).TotalMinutes)
            {
                result.Message = "The appointment time is less than service duration time";
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }

            var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date, medicalStaffId: request.DTO.MedicalStaffId).Result;
            var invalidAppointment = staffExistingAppointments.FirstOrDefault(x => !TimeHelper.isTimeValid(x, request.StartTime, request.EndTime));

            if (invalidAppointment != null)
            {
                result.Message = "The staff isn't free in requested Time";
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }

            if (request.DeviceId != null)
            {
                var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(request.Date, deviceId: request.DeviceId, roomId: request.RoomId).Result;
                invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !TimeHelper.isTimeValid(x, request.StartTime, request.EndTime));

                if (invalidAppointment != null)
                {
                    result.Message = "The room or device isn't free in requested Time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeeded",
                        AdditionalData = result
                    });

                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
                }
            }

            result.Message = "The time is valid";
            result.IsValid = true;
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = result
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
        }
    }
}