using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
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
    public class SearchByRequestedFieldsQueryHandler : IRequestHandler<SearchByRequestedFieldsQuery, BaseResponse>
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

        public SearchByRequestedFieldsQueryHandler(
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

        public async Task<BaseResponse> Handle(SearchByRequestedFieldsQuery request, CancellationToken cancellationToken)
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

                return responseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    appointments);
            }

            if (request.DTO.FilterFields.Count == 1)
            {
                if (isServiceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date.Date, serviceId: request.DTO.FilterFields[0].ServiceId).Result;
                }

                if (isStaffRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date.Date, medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId).Result;
                }

                if (isDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndDevice(request.DTO.Date.Date, deviceId: request.DTO.FilterFields[0].DeviceId).Result;
                }

                if (isStaffAndDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByStaffAndDevice(
                        request.DTO.Date.Date,
                        medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId,
                        deviceId: request.DTO.FilterFields[0].DeviceId).Result;
                }

                if (isServiceAndDeviceRequested)
                {
                    appointments = _appointmentRepository.GetByServiceAndDevice(
                        request.DTO.Date.Date,
                        serviceId: request.DTO.FilterFields[0].ServiceId,
                        deviceId: request.DTO.FilterFields[0].DeviceId).Result;

                }

                if (isAllRequested || isStaffAndServiceRequested)
                {
                    appointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date.Date, medicalStaffId: request.DTO.FilterFields[0].MedicalStaffId).Result;

                    if (request.DTO.FilterFields[0].DeviceId != null)
                    {
                        var deviceAppointments = _appointmentRepository.GetByDateAndDevice(request.DTO.Date.Date, deviceId: request.DTO.FilterFields[0].DeviceId).Result
                            .FindAll(x => x.MedicalStaffId != request.DTO.FilterFields[0].MedicalStaffId).ToList();

                        if (deviceAppointments.Count != 0)
                        {
                            appointments.AddRange(deviceAppointments);
                        }
                    }

                    var serviceName = _serviceRepository.GetById((Guid)request.DTO.FilterFields[0].ServiceId).Result.Name;

                    var staffSchedule = _staffScheduleRepository.GetStaffScheduleByDate(
                        request.DTO.FilterFields[0].MedicalStaffId,
                        request.DTO.Date.DayOfWeek).Result;

                    var service = _serviceDurationRepository.GetByServiceAndStaffId(
                        request.DTO.FilterFields[0].MedicalStaffId,
                        request.DTO.FilterFields[0].ServiceId).Result;

                    if (staffSchedule != null)
                    {
                        var staffFreeTimes = new List<time>();

                        if (appointments != null)
                        {
                            var times = new List<TimeOnly>();
                            for (int i = 0; i < appointments.Count; i++)
                            {
                                if (i == 0 &&
                                TimeOnly.Parse(appointments[i].StartTime) > TimeOnly.Parse(staffSchedule.MorningStart))
                                {
                                    var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(staffSchedule.MorningStart),
                                    TimeOnly.Parse(appointments[i].StartTime)
                                };
                                    times.AddRange(freeTimes);
                                }
                                if (i == appointments.Count - 1 &&
                                    TimeOnly.Parse(appointments[i].EndTime) < TimeOnly.Parse(staffSchedule.EveningEnd))
                                {
                                    var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(appointments[i].EndTime),
                                    TimeOnly.Parse(staffSchedule.EveningEnd)
                                };
                                    times.AddRange(freeTimes);
                                }
                                if (i != 0 && i != appointments.Count - 1)
                                {
                                    var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(appointments[i - 1].EndTime),
                                    TimeOnly.Parse(appointments[i].StartTime)
                                };
                                    times.AddRange(freeTimes);
                                }
                            }

                            times.OrderBy(d => d);
                            //foreach (var item in times)
                            //{
                            //    if (item.IsBetween
                            //        (TimeOnly.Parse(staffSchedule.MorningEnd),
                            //        TimeOnly.Parse(staffSchedule.EveningStart)))
                            //    {
                            //        times.Remove(item);
                            //    }
                            //}

                            for (int i = 0; i < times.Count; i++)
                            {
                                freeTimeGenerator(staffFreeTimes, service.Duration, times[i], times[i + 1], staffSchedule);
                            }
                        }

                        else
                        {
                            var morningStart = TimeOnly.Parse(staffSchedule.MorningStart);
                            var morningEnd = TimeOnly.Parse(staffSchedule.MorningEnd);
                            var eveningStart = TimeOnly.Parse(staffSchedule.EveningStart);
                            var eveningEnd = TimeOnly.Parse(staffSchedule.EveningEnd);

                            freeTimeGenerator(staffFreeTimes, service.Duration, morningStart, morningEnd, staffSchedule);
                            freeTimeGenerator(staffFreeTimes, service.Duration, eveningStart, eveningEnd, staffSchedule);
                        }

                        foreach (var item in staffFreeTimes)
                        {
                            appointments.Add(new AppointmentDetailsDTO
                            {
                                StaffName = staffSchedule.StaffName,
                                StaffLastName = staffSchedule.StaffLastName,
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

                return responseBuilder.Success(HttpStatusCode.OK,
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

                return responseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed");
            }
        }

        private static void freeTimeGenerator(List<time> staffFreeTimes, int serviceDuration, TimeOnly startTime, TimeOnly endTime, MedicalStaffScheduleDayOfWeekDTO staffSchedule)
        {
            int x = 0;
            x += (int)(endTime - startTime).TotalMinutes / serviceDuration;

            time[] freeTimes = new time[x];

            for (int j = 0; j < freeTimes.Length; j++)
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

                if (freeTimes[j].StartTime.IsBetween(TimeOnly.Parse(staffSchedule.MorningEnd), TimeOnly.Parse(staffSchedule.EveningStart)) ||
                    freeTimes[j].EndTime.IsBetween(TimeOnly.Parse(staffSchedule.MorningEnd), TimeOnly.Parse(staffSchedule.EveningStart)))
                {
                    continue;
                }
                else
                {
                    staffFreeTimes.Add(freeTimes[j]);
                }
            }
        }

        private class time
        {
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
        }

        public class Range<T> where T : IComparable
        {
            readonly T min;
            readonly T max;

            public Range(T min, T max)
            {
                this.min = min;
                this.max = max;
            }

            public bool IsOverlapped(Range<T> other)
            {
                return Min.CompareTo(other.Max) < 0 && other.Min.CompareTo(Max) < 0;
            }

            public T Min { get { return min; } }
            public T Max { get { return max; } }
        }
    }
}