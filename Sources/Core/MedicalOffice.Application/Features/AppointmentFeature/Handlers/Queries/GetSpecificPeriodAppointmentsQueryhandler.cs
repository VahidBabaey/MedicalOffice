using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class GetSpecificPeriodAppointmentsQueryhandler : IRequestHandler<GetSpecificPeriodAppointmentsQuery, BaseResponse>
    {
        private readonly IValidator<SpecificPeriodAppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffScheduleRepository _staffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;
        private readonly IDeviceRepository _deviceRepository;

        private readonly string _requestTitle;

        public GetSpecificPeriodAppointmentsQueryhandler(
            IValidator<SpecificPeriodAppointmentDTO> validator,
            ILogger logger,
            IAppointmentRepository appointmentRepository,
            IServiceRepository serviceRepository,
            IMedicalStaffScheduleRepository staffScheduleRepository,
            IServiceDurationRepositopry serviceDurationRepository,
            IDeviceRepository deviceRepository)
        {
            _validator = validator;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepository;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _deviceRepository = deviceRepository;
        }

        public async Task<BaseResponse> Handle(GetSpecificPeriodAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var result = new List<SpecificPeriodAppointmentResDTO>();

            //TODO: it should go to validator
            if (request.DTO.RoomId != null && request.DTO.DeviceId != null)
            {
                var roomHasDevice = _deviceRepository.GetDevicesByRoomId((Guid)request.DTO.RoomId).Result
                    .Contains(new Device { Id = (Guid)request.DTO.DeviceId });

                if (!roomHasDevice)
                {
                    var error = "Device isn't exist in this room";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            if (request.DTO.MedicalStaffId == null && request.DTO.DeviceId == null)
            {
                //TODO: It should go to validator
                var existingService = _serviceRepository.GetById(request.DTO.ServiceId);
                if (existingService == null)
                {
                    var error = "Service isn't exist in this room";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                var serviceDuration = _serviceDurationRepository.GetAllBySearchClause(new { ServiceId = request.DTO.ServiceId }).Result.ToList();

                var medicalStaffIds = serviceDuration.Select(d => d.MedicalStaffId).ToList();

                var allStaffAppointments = new List<SpecificPeriodAppointmentResDTO>();

                foreach (var staffId in medicalStaffIds)
                {
                    var eachStaffAppointments = new List<SpecificPeriodAppointmentResDTO>();

                    var service = _serviceDurationRepository
                        .GetByServiceAndStaffId(serviceId: request.DTO.ServiceId, medicalStaffId: staffId).Result;
                    if (service == null)
                    {
                        continue;
                    }

                    var appointments = new List<Appointment>();
                    appointments = _appointmentRepository.GetByPeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, staffId).Result.ToList();

                    var staffSchedule = new List<MedicalStaffSchedule>();
                    staffSchedule = _staffScheduleRepository.GetAllBySearchClause(new MedicalStaffSchedule { MedicalStaffId = staffId }).Result.ToList();

                    eachStaffPeriodAppointmetsCounts(eachStaffAppointments, service, appointments, staffSchedule);

                    allStaffAppointments.AddRange(eachStaffAppointments);
                }

                var dateAppointments = new List<SpecificPeriodAppointmentResDTO>();

                var appointmentGroups = allStaffAppointments.OrderBy(a => a.Date)
                    .GroupBy(x => x.Date);

                foreach (var group in appointmentGroups)
                {
                    var dateAppointment = new SpecificPeriodAppointmentResDTO();

                    dateAppointment.Date = group.Key;
                    foreach (var appointment in group)
                    {
                        dateAppointment.AllTimes = +appointment.AllTimes;
                        dateAppointment.FullTimes = +appointment.FullTimes;
                        dateAppointment.FreeTimes.AddRange(appointment.FreeTimes);
                    }

                    dateAppointments.Add(dateAppointment);
                }

                result.AddRange(dateAppointments);
            }

            if (request.DTO.MedicalStaffId != null && request.DTO.DeviceId == null)
            {
                var service = _serviceDurationRepository
                    .GetByServiceAndStaffId(serviceId: request.DTO.ServiceId, medicalStaffId: request.DTO.MedicalStaffId).Result;
                if (service == null)
                {
                    throw new ArgumentException("There is no service for this staff");
                }

                var appointments = new List<Appointment>();
                appointments = _appointmentRepository
                    .GetByPeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, request.DTO.MedicalStaffId).Result.ToList();

                var staffSchedule = new List<MedicalStaffSchedule>();
                staffSchedule = _staffScheduleRepository.GetMedicalStaffScheduleByID(request.DTO.MedicalStaffId).Result.ToList();

                if (staffSchedule == null)
                {
                    throw new ArgumentException("There is no schedule for this staff");
                }
                eachStaffPeriodAppointmetsCounts(result, service, appointments, staffSchedule);
            }

            if (request.DTO.DeviceId != null)
            {
                var service = _serviceDurationRepository
                    .GetByServiceAndStaffId(serviceId: request.DTO.ServiceId, medicalStaffId: request.DTO.MedicalStaffId).Result;
                if (service == null)
                {
                    var error = "Service isn't exist";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                var appointments = new List<Appointment>();
                appointments = _appointmentRepository
                    .GetByPeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, request.DTO.MedicalStaffId).Result.ToList();

                var deviceAppointments = new List<Appointment>();
                deviceAppointments = _appointmentRepository
                 .GetByPeriodAndDeviceId(request.DTO.StartDate, request.DTO.EndDate, request.DTO.DeviceId).Result.ToList();

                appointments.AddRange(deviceAppointments);

                var staffSchedule = new List<MedicalStaffSchedule>();
                staffSchedule = _staffScheduleRepository.GetMedicalStaffScheduleByID(request.DTO.MedicalStaffId).Result.ToList();

                if (staffSchedule == null)
                {
                    var error = "There is no schedule for this staff";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                eachStaffPeriodAppointmetsCounts(result, service, appointments, staffSchedule);
            }

            throw new NotImplementedException();
        }

        private static void eachStaffPeriodAppointmetsCounts(
            List<SpecificPeriodAppointmentResDTO> result,
            ServiceNameDurationDTO service,
            List<Appointment> appointments,
            List<MedicalStaffSchedule> staffSchedule)
        {
            foreach (var daySchedule in staffSchedule)
            {
                var eachStaffDayOfWeekAppointments = new SpecificPeriodAppointmentResDTO();

                var staffDayOfWeekAppointments = appointments.FindAll(x => x.Date.DayOfWeek == daySchedule.WeekDay).ToList();

                var staffFreeTimes = new List<time>();
                if (staffDayOfWeekAppointments != null)
                {
                    var times = new List<TimeOnly>();
                    for (int i = 0; i < appointments.Count; i++)
                    {
                        if (i == 0 &&
                        TimeOnly.Parse(appointments[i].StartTime) > TimeOnly.Parse(daySchedule.MorningStart))
                        {
                            var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(daySchedule.MorningStart),
                                    TimeOnly.Parse(appointments[i].StartTime)
                                };
                            times.AddRange(freeTimes);
                        }
                        else if (i == appointments.Count - 1 &&
                            TimeOnly.Parse(appointments[i].EndTime) < TimeOnly.Parse(daySchedule.EveningEnd))
                        {
                            var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(appointments[i].EndTime),
                                    TimeOnly.Parse(daySchedule.EveningEnd)
                                };
                            times.AddRange(freeTimes);
                        }
                        else
                        {
                            var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(appointments[i - 1].EndTime),
                                    TimeOnly.Parse(appointments[i].StartTime)
                                };
                            times.AddRange(freeTimes);
                        }
                    }

                    foreach (var time in times)
                    {
                        if (time.IsBetween
                            (TimeOnly.Parse(daySchedule.MorningEnd),
                            TimeOnly.Parse(daySchedule.EveningStart)))
                        {
                            times.Remove(time);
                        }
                    }

                    for (int i = 0; i < times.Count; i++)
                    {
                        freeTimeGenerator(staffFreeTimes, service.Duration, times[i], times[i + 1]);
                    }
                }

                else
                {
                    var morningStart = TimeOnly.Parse(daySchedule.MorningStart);
                    var morningEnd = TimeOnly.Parse(daySchedule.MorningEnd);
                    var eveningStart = TimeOnly.Parse(daySchedule.EveningStart);
                    var eveningEnd = TimeOnly.Parse(daySchedule.EveningEnd);

                    freeTimeGenerator(staffFreeTimes, service.Duration, morningStart, morningEnd);
                    freeTimeGenerator(staffFreeTimes, service.Duration, eveningStart, eveningEnd);
                }

                foreach (var freeTime in staffFreeTimes)
                {
                    eachStaffDayOfWeekAppointments.FreeTimes.Add(new FreeTime(freeTime.StartTime, freeTime.EndTime));
                }

                eachStaffDayOfWeekAppointments.Date = staffDayOfWeekAppointments[0].Date;
                eachStaffDayOfWeekAppointments.AllTimes = staffFreeTimes.Count + staffDayOfWeekAppointments.Count;
                eachStaffDayOfWeekAppointments.FullTimes = staffDayOfWeekAppointments.Count;
                if (eachStaffDayOfWeekAppointments.AllTimes == eachStaffDayOfWeekAppointments.FullTimes)
                    eachStaffDayOfWeekAppointments.Full = true;

                result.Add(eachStaffDayOfWeekAppointments);
            }
        }

        private static void freeTimeGenerator(List<time> staffFreeTimes, int serviceDuration, TimeOnly startTime, TimeOnly endTime)
        {
            int x = 0;
            x += (int)(endTime - startTime).TotalMinutes / serviceDuration;

            time[] freeTimes = new time[x];

            for (int j = 0; j <= freeTimes.Length; j++)
            {
                if (j == 0)
                {
                    freeTimes[j] = new time
                    {
                        StartTime = startTime,
                        EndTime = startTime.AddMinutes(serviceDuration)
                    };
                }
                else
                {
                    freeTimes[j] = new time
                    {
                        StartTime = freeTimes[j - 1].EndTime,
                        EndTime = freeTimes[j - 1].EndTime.AddMinutes(serviceDuration)
                    };
                }

                staffFreeTimes.Add(freeTimes[j]);
            }
        }
        private class time
        {
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
        }
    }
}
