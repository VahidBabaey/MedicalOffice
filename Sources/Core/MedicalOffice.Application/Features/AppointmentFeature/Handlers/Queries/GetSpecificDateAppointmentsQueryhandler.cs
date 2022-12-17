using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class GetSpecificDateAppointmentsQueryhandler : IRequestHandler<GetSpecificDateAppointmentsQuery, BaseResponse>
    {
        private readonly IValidator<SpecificDateAppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffScheduleRepository _staffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

        private readonly string _requestTitle;

        public GetSpecificDateAppointmentsQueryhandler(
            IValidator<SpecificDateAppointmentDTO> validator,
            ILogger logger, IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IMedicalStaffScheduleRepository staffScheduleRepository,
            IServiceDurationRepositopry serviceDurationRepository)
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepository;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetSpecificDateAppointmentsQuery request, CancellationToken cancellationToken)
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

            if (request.DTO.MedicalStaffId == null && request.DTO.DeviceId == null)
            {
                var existingService = _serviceRepository.GetById(request.DTO.ServiceId);
                if (existingService == null)
                    throw new ArgumentException("Sevice id doesnt exist!");

                var serviceDuration = _serviceDurationRepository.GetAllBySearchClause(new { ServiceId = request.DTO.ServiceId }).Result.ToList();

                var medicalStaffIds = serviceDuration.Select(d => d.MedicalStaffId).ToList();

                var result = new List<DateAppointmentDTO>();
                foreach (var item in medicalStaffIds)
                {
                    var appointments = new List<Appointment>();
                    appointments = _appointmentRepository.GetByTimePeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, item).Result.ToList();

                    var staffSchedule = new List<MedicalStaffSchedule>();
                    staffSchedule = _staffScheduleRepository.GetAllBySearchClause(new MedicalStaffSchedule { MedicalStaffId = item }).Result.ToList();

                    var service = serviceDuration.FirstOrDefault(x => x.MedicalStaffId == item);

                    var staffDateAppointment = new List<DateAppointmentDTO>();
                    foreach (var schedule in staffSchedule)
                    {
                        var itemResult = new DateAppointmentDTO();

                        var dayOfWeekAppointment = appointments.FindAll(x => x.Date.DayOfWeek == schedule.WeekDay).ToList();
                        var dayOfWeekAppointmentCount = 0;

                        var staffFreeTimes = new List<time>();
                        if (dayOfWeekAppointment != null)
                        {
                            dayOfWeekAppointmentCount = dayOfWeekAppointment.Count();

                            var times = new List<TimeOnly>();
                            for (int i = 0; i < appointments.Count; i++)
                            {
                                if (i == 0 &&
                                TimeOnly.Parse(appointments[i].StartTime) > TimeOnly.Parse(schedule.MorningStart))
                                {
                                    var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(schedule.MorningStart),
                                    TimeOnly.Parse(appointments[i].StartTime)
                                };
                                    times.AddRange(freeTimes);
                                }
                                else if (i == appointments.Count - 1 &&
                                    TimeOnly.Parse(appointments[i].EndTime) < TimeOnly.Parse(schedule.EveningEnd))
                                {
                                    var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(appointments[i].EndTime),
                                    TimeOnly.Parse(schedule.EveningEnd)
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
                                    (TimeOnly.Parse(schedule.MorningEnd),
                                    TimeOnly.Parse(schedule.EveningStart)))
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
                            var morningStart = TimeOnly.Parse(schedule.MorningStart);
                            var morningEnd = TimeOnly.Parse(schedule.MorningEnd);
                            var eveningStart = TimeOnly.Parse(schedule.EveningStart);
                            var eveningEnd = TimeOnly.Parse(schedule.EveningEnd);

                            freeTimeGenerator(staffFreeTimes, service.Duration, morningStart, morningEnd);
                            freeTimeGenerator(staffFreeTimes, service.Duration, eveningStart, eveningEnd);
                        }

                        foreach (var freeTime in staffFreeTimes)
                        {
                            itemResult.FreeTimes.Add(new FreeTime(freeTime.StartTime, freeTime.EndTime));
                        }

                        itemResult.Date = dayOfWeekAppointment[0].Date;
                        itemResult.AllTimes = staffFreeTimes.Count + dayOfWeekAppointment.Count;
                        itemResult.FullTimes = dayOfWeekAppointment.Count;
                        if (itemResult.AllTimes == itemResult.FullTimes)
                            itemResult.Full = true;

                        staffDateAppointment.Add(itemResult);
                    }

                    var groupAppointments = staffDateAppointment.OrderBy(a => a.Date).GroupBy(x => x.Date);

                    var dateAppointments = new List<DateAppointmentDTO>();
                    foreach (var group in groupAppointments)
                    {
                        var dateAppointment = new DateAppointmentDTO();

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
            }
            throw new NotImplementedException();
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
