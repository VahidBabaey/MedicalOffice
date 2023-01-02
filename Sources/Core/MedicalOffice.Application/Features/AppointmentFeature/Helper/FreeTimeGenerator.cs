using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Helper
{
    public static class FreeTimeGenerator
    {
        public static void GenerateServiceFreeTimes(List<time> staffFreeTimes, int serviceDuration, TimeOnly startTime, TimeOnly endTime, MedicalStaffSchedule staffSchedule)
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

        public static void GenerateStaffTotalContinuesFreeTimes(List<AppointmentDetailsDTO> appointments, MedicalStaffSchedule staffSchedule, List<TimeOnly> times)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (i == 0 && TimeOnly.Parse(appointments[i].StartTime) > TimeOnly.Parse(staffSchedule.MorningStart))
                {
                    var freeTimes = new List<TimeOnly> {
                        TimeOnly.Parse(staffSchedule.MorningStart),
                        TimeOnly.Parse(appointments[i].StartTime)};
                    times.AddRange(freeTimes);
                }

                if (i == appointments.Count - 1 && TimeOnly.Parse(appointments[i].EndTime) < TimeOnly.Parse(staffSchedule.EveningEnd))
                {
                    var freeTimes = new List<TimeOnly> {
                        TimeOnly.Parse(appointments[i].EndTime),
                        TimeOnly.Parse(staffSchedule.EveningEnd)};
                    times.AddRange(freeTimes);
                }

                if (i != 0 && i != appointments.Count - 1)
                {
                    var freeTimes = new List<TimeOnly> {
                        TimeOnly.Parse(appointments[i - 1].EndTime),
                        TimeOnly.Parse(appointments[i].StartTime)};
                    times.AddRange(freeTimes);
                }
            }
        }

        public static void StaffPeriodAppointmetsCounts(
            List<SpecificPeriodAppointmentResDTO> result,
            ServiceDurationDetailsDTO service,
            List<AppointmentDetailsDTO> appointments,
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
                    GenerateStaffTotalContinuesFreeTimes(appointments, daySchedule, times);

                    for (int i = 0; i < times.Count - 1; i++)
                    {
                        GenerateServiceFreeTimes(staffFreeTimes, service.Duration, times[i], times[i + 1], daySchedule);
                    }
                }

                else
                {
                    var morningStart = TimeOnly.Parse(daySchedule.MorningStart);
                    var morningEnd = TimeOnly.Parse(daySchedule.MorningEnd);
                    var eveningStart = TimeOnly.Parse(daySchedule.EveningStart);
                    var eveningEnd = TimeOnly.Parse(daySchedule.EveningEnd);

                    GenerateServiceFreeTimes(staffFreeTimes, service.Duration, morningStart, morningEnd, daySchedule);
                    GenerateServiceFreeTimes(staffFreeTimes, service.Duration, eveningStart, eveningEnd, daySchedule);
                }

                var freeTimes = new List<FreeTime>();
                foreach (var freeTime in staffFreeTimes)
                {
                    var time = new FreeTime(TimeOnly.Parse("7:00"), TimeOnly.Parse("7:30"));
                    freeTimes.Add(time);
                }

                eachStaffDayOfWeekAppointments.FreeTimes = freeTimes;

                if (staffDayOfWeekAppointments != null)
                {
                    eachStaffDayOfWeekAppointments.Date = staffDayOfWeekAppointments[0].Date;
                    eachStaffDayOfWeekAppointments.AllTimes = staffFreeTimes.Count + staffDayOfWeekAppointments.Count;
                    eachStaffDayOfWeekAppointments.FullTimes = staffDayOfWeekAppointments.Count;
                    if (eachStaffDayOfWeekAppointments.AllTimes == eachStaffDayOfWeekAppointments.FullTimes)
                        eachStaffDayOfWeekAppointments.Full = true;

                    result.Add(eachStaffDayOfWeekAppointments);
                }
            }
        }
    }
}
