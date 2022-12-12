using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
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
        private readonly string _requestTitle;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

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

            if (request.DTO.FilterFields.Count == 1)
            {

            }
            var appointments = new List<IReadOnlyList<Appointment>>();
            foreach (var requestedField in request.DTO.FilterFields)
            {
                //Get all appointments
                var existingAppointments = _appointmentRepository
                    .GetAllBySearchClause(new { requestedField, request.DTO.Date }).Result
                    .OrderBy(x => TimeOnly.Parse(x.StartTime)).ToList();

                //Create staffFreeTimes object for each requested field
                var staffFreeTimes = _mapper.Map<StaffFreeTimes>(requestedField);

                //Get staff schedule by  
                var staffSchedule = _staffScheduleRepository.GetAllBySearchClause(new
                {
                    requestedField.MedicalStaffId,
                    request.DTO.Date.DayOfWeek
                });

                var serviceDuration = _serviceDurationRepository.GetAllBySearchClause(new
                {
                    requestedField.MedicalStaffId,
                    requestedField.ServiceId
                }).Result.Select(x => x.Duration).First();

                if (staffSchedule != null)
                {
                    var staffShift = _mapper.Map<staffShift>(staffSchedule);
                    if (existingAppointments != null)
                    {
                        var times = new List<TimeOnly>();
                        for (int i = 0; i < existingAppointments.Count; i++)
                        {
                            if (i == 0 &&
                                TimeOnly.Parse(existingAppointments[i].StartTime) > TimeOnly.Parse(staffShift.MorningStart))
                            {
                                var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(staffShift.MorningStart),
                                    TimeOnly.Parse(existingAppointments[i].StartTime)
                                };
                                times.AddRange(freeTimes);
                            }
                            else if (i == existingAppointments.Count - 1 &&
                                TimeOnly.Parse(existingAppointments[i].EndTime) < TimeOnly.Parse(staffShift.EveningEnd))
                            {
                                var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(existingAppointments[i].EndTime),
                                    TimeOnly.Parse(staffShift.EveningEnd)
                                };
                                times.AddRange(freeTimes);
                            }
                            else
                            {
                                var freeTimes = new List<TimeOnly> {
                                    TimeOnly.Parse(existingAppointments[i - 1].EndTime),
                                    TimeOnly.Parse(existingAppointments[i].StartTime)
                                };
                                times.AddRange(freeTimes);
                            }
                        }

                        foreach (var item in times)
                        {
                            if (item.IsBetween
                                (TimeOnly.Parse(staffShift.MorningEnd),
                                TimeOnly.Parse(staffShift.EveningStart)))
                            {
                                times.Remove(item);
                            }
                        }

                        for (int i = 0; i < times.Count; i++)
                        {
                            freeTimeGenerator(staffFreeTimes, serviceDuration, times[i], times[i + 1]);
                            //int x = 0;
                            //x += (int)(times[i + 1] - times[i]).TotalMinutes / serviceDuration;

                            //time[] freeTimes = new time[x];

                            //for (int j = 0; j <= freeTimes.Length; j++)
                            //{
                            //    if (j == 0)
                            //    {
                            //        freeTimes[j] = new time
                            //        {
                            //            StartTime = times[i],
                            //            EndTime = times[i].AddMinutes(serviceDuration)
                            //        };
                            //    }
                            //    else
                            //    {
                            //        freeTimes[j] = new time
                            //        {
                            //            StartTime = freeTimes[j - 1].EndTime,
                            //            EndTime = freeTimes[j - 1].EndTime.AddMinutes(serviceDuration)
                            //        };
                            //    }

                            //    staffFreeTimes.FreeTimes.Add(freeTimes[j]);
                            //}
                        }
                    }
                    else
                    {
                        var morningStart = TimeOnly.Parse(staffSchedule.Result.Select(x => x.MorningStart).First());
                        var morningEnd = TimeOnly.Parse(staffSchedule.Result.Select(x => x.MorningEnd).First());
                        var eveningStart = TimeOnly.Parse(staffSchedule.Result.Select(x => x.EveningStart).First());
                        var eveningEnd = TimeOnly.Parse(staffSchedule.Result.Select(x => x.EveningEnd).First());

                        freeTimeGenerator(staffFreeTimes, serviceDuration, morningStart, morningEnd);
                        freeTimeGenerator(staffFreeTimes, serviceDuration, eveningStart, eveningEnd);
                    }
                }

            }


            throw new NotImplementedException();
        }

        private static void freeTimeGenerator(StaffFreeTimes staffFreeTimes, int serviceDuration, TimeOnly startTime, TimeOnly endTime)
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

                staffFreeTimes.FreeTimes.Add(freeTimes[j]);
            }
        }

        private class StaffFreeTimes
        {
            public Guid MedicalStaffId { get; set; }
            public Guid ServiceId { get; set; }
            public Guid RoomId { get; set; }
            public List<time> FreeTimes { get; set; }
        }

        private class time
        {
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
        }

        private class staffShift
        {
            public string MorningStart { get; set; }
            public string MorningEnd { get; set; }
            public string EveningStart { get; set; }
            public string EveningEnd { get; set; }
        }
    }
}