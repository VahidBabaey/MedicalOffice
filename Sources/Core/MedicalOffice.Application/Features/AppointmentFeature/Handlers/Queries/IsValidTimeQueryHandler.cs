using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class IsValidTimeQueryHandler : IRequestHandler<IsValidTimeQuery, BaseResponse>
    {
        private readonly IValidator<AppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IMedicalStaffScheduleRepository _medicalStaffScheduleRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

        private readonly string _requestTitle;

        public IsValidTimeQueryHandler(
            IValidator<AppointmentDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IMedicalStaffScheduleRepository medicalStaffScheduleRepository,
            IServiceRepository serviceRepository,
            IServiceDurationRepositopry serviceDurationRepositopry
            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _medicalStaffScheduleRepository = medicalStaffScheduleRepository;
            _serviceRepository = serviceRepository;
            _serviceDurationRepository = serviceDurationRepositopry;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(IsValidTimeQuery request, CancellationToken cancellationToken)
        {

            var responseBuilder = new ResponseBuilder();

            var validTime = _medicalStaffScheduleRepository.CheckTimeIsInStaffSchedule((Guid)request.MedicalStaffId, request.Date).Result;

            if (!validTime)
                throw new ArgumentException("");

            var service = _serviceDurationRepository.GetByServiceAndStaffId(request.MedicalStaffId, request.ServiceId).Result;
            if (service == null)
            {
                throw new ArgumentException("");
            }

            if (service.Duration < (TimeOnly.Parse(request.EndTime) - TimeOnly.Parse(request.StartTime)).TotalMinutes)
                throw new ArgumentException("");

            var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(request.Date, medicalStaffId: request.MedicalStaffId).Result;
            var invalidAppointment = staffExistingAppointments.FirstOrDefault(x => !isValidTime(x, request.StartTime, request.EndTime));

            if (invalidAppointment != null)
            {
                var error = "Staff isn't free in requested time";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DeviceId != null)
            {
                var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(request.Date, deviceId: request.DeviceId, roomId: request.RoomId).Result;
                invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !isValidTime(x, request.StartTime, request.EndTime));

                if (invalidAppointment != null)
                {
                    var error = "Room or device isn't free in requested time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            throw new NotImplementedException();
        }

        private static bool isValidTime(AppointmentDetailsDTO time, string startTime, string endTime)
        {
            var serviceTime = (TimeOnly.Parse(endTime) - TimeOnly.Parse(startTime)).TotalMinutes;

            if (TimeOnly.Parse(time.StartTime) < TimeOnly.Parse(endTime) &&
                TimeOnly.Parse(startTime) < TimeOnly.Parse(time.EndTime))
                return false;

            if (TimeOnly.Parse(time.StartTime) == TimeOnly.Parse(startTime) ||
                TimeOnly.Parse(time.EndTime) == TimeOnly.Parse(endTime))
                return false;

            if (
                (TimeOnly.Parse(startTime) - TimeOnly.Parse(time.StartTime)).TotalMinutes < serviceTime ||
                (TimeOnly.Parse(endTime) - TimeOnly.Parse(time.EndTime)).TotalMinutes < serviceTime)
                return false;

            return true;
        }
    }
}