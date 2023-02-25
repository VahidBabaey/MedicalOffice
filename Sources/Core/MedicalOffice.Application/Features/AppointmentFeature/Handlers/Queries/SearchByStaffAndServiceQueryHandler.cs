using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Handlers.Helper;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
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
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public partial class SearchByStaffAndServiceQueryHandler : IRequestHandler<SearchByStaffAndServiceQuery, BaseResponse>
    {
        private readonly IValidator<SearchAppointmentsDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffScheduleRepository _staffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

        private readonly string _requestTitle;

        public SearchByStaffAndServiceQueryHandler(
            IValidator<SearchAppointmentsDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IMedicalStaffScheduleRepository staffScheduleRepository,
            IServiceDurationRepositopry serviceDurationRepositopry

            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepositopry;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(SearchByStaffAndServiceQuery request, CancellationToken cancellationToken)
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

            bool isServiceRequested =
                request.DTO.FilterFields[0].MedicalStaffId == null &&
                request.DTO.FilterFields[0].ServiceId != null &&
                request.DTO.FilterFields[0].DeviceId == null;

            bool isStaffRequested =
                request.DTO.FilterFields[0].MedicalStaffId != null &&
                request.DTO.FilterFields[0].ServiceId == null &&
                request.DTO.FilterFields[0].DeviceId == null;

            bool isDeviceRequested =
                request.DTO.FilterFields[0].MedicalStaffId == null &&
                request.DTO.FilterFields[0].ServiceId == null &&
                request.DTO.FilterFields[0].DeviceId != null;

            bool isStaffAndServiceRequested =
                request.DTO.FilterFields[0].MedicalStaffId != null &&
                request.DTO.FilterFields[0].ServiceId != null &&
                request.DTO.FilterFields[0].DeviceId == null;

            bool isStaffAndDeviceRequested =
                request.DTO.FilterFields[0].MedicalStaffId != null &&
                request.DTO.FilterFields[0].ServiceId == null &&
                request.DTO.FilterFields[0].DeviceId != null;

            bool isServiceAndDeviceRequested =
                request.DTO.FilterFields[0].MedicalStaffId == null &&
                request.DTO.FilterFields[0].ServiceId != null &&
                request.DTO.FilterFields[0].DeviceId != null;

            bool isAllRequested =
                request.DTO.FilterFields[0].MedicalStaffId != null &&
                request.DTO.FilterFields[0].ServiceId != null &&
                request.DTO.FilterFields[0].DeviceId != null;

            var appointments = new List<AppointmentDetailsDTO>();
            if (request.DTO.FilterFields.Count == 0)
            {
                appointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date.Date).Result;

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = appointments
                });

                return ResponseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    appointments);
            }

            if (request.DTO.FilterFields.Count == 1)
            {
                if (isServiceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(
                        request.DTO.Date.Date, 
                        serviceId: request.DTO.FilterFields[0].ServiceId)
                        .Result;
                }

                if (isStaffRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(
                        request.DTO.Date.Date, 
                        medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId)
                        .Result;
                }

                if (isDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndDevice(
                        request.DTO.Date.Date, 
                        deviceId: request.DTO.FilterFields[0].DeviceId)
                        .Result;
                }

                if (isStaffAndDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByStaffAndDevice(
                        request.DTO.Date.Date,
                        medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId,
                        deviceId: request.DTO.FilterFields[0].DeviceId)
                        .Result;
                }

                if (isServiceAndDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByServiceAndDevice(
                        request.DTO.Date.Date,
                        serviceId: request.DTO.FilterFields[0].ServiceId,
                        deviceId: request.DTO.FilterFields[0].DeviceId)
                        .Result;
                }

                if (isAllRequested || isStaffAndServiceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(
                        request.DTO.Date.Date, 
                        medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId)
                        .Result;

                    var staffSchedule = _staffScheduleRepository.GetStaffScheduleByDate(
                        request.DTO.FilterFields[0].MedicalStaffId,
                        request.DTO.Date.DayOfWeek)
                        .Result;

                    var service = _serviceDurationRepository.GetByServiceAndStaffId(
                        request.DTO.FilterFields[0].MedicalStaffId,
                        request.DTO.FilterFields[0].ServiceId)
                        .Result;

                    if (request.DTO.FilterFields[0].DeviceId != null)
                    {
                        var deviceAppointments = _appointmentRepository.GetByDateAndDevice(
                            request.DTO.Date.Date, 
                            deviceId: request.DTO.FilterFields[0].DeviceId).Result
                            .FindAll(x => x.MedicalStaffId != request.DTO.FilterFields[0].MedicalStaffId).ToList();

                        if (deviceAppointments.Count != 0)
                        {
                            appointments.AddRange(deviceAppointments);
                        }
                    }

                    if (staffSchedule != null)
                    {
                        var staffFreeTimes = new List<Time>();

                        if (appointments.Count != 0)
                        {
                            var times = new List<TimeOnly>();
                            FreeTimeGenerator.GenerateStaffTotalContinuesFreeTimes(appointments, staffSchedule, times);

                            times.OrderBy(d => d);

                            for (int i = 0; i < times.Count - 1; i++)
                            {
                                FreeTimeGenerator.GenerateServiceFreeTimes(staffFreeTimes, service.Duration, times[i], times[i + 1], staffSchedule);
                            }
                        }

                        else
                        {
                            var morningStart = TimeOnly.Parse(staffSchedule.MorningStart);
                            var morningEnd = TimeOnly.Parse(staffSchedule.MorningEnd);
                            var eveningStart = TimeOnly.Parse(staffSchedule.EveningStart);
                            var eveningEnd = TimeOnly.Parse(staffSchedule.EveningEnd);

                            FreeTimeGenerator.GenerateServiceFreeTimes(staffFreeTimes, service.Duration, morningStart, morningEnd, staffSchedule);
                            FreeTimeGenerator.GenerateServiceFreeTimes(staffFreeTimes, service.Duration, eveningStart, eveningEnd, staffSchedule);
                        }

                        foreach (var item in staffFreeTimes)
                        {
                            appointments.Add(new AppointmentDetailsDTO
                            {
                                MedicalStaffId = service.MedicalStaffId,
                                StaffName = service.StaffName,
                                StartTime = item.StartTime.ToString(),
                                EndTime = item.EndTime.ToString(),
                                ServiceName = service.ServiceName
                            });
                        }
                    }
                }

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = appointments
                });

                return ResponseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    appointments);
            }

            else
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed"
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed");
            }
        }   
    }
}