using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Helper
{
    public static class FreeTimeGenerator
    {
        public static void GenerateServiceFreeTimes(List<time> staffFreeTimes, int serviceDuration, TimeOnly startTime, TimeOnly endTime, StaffDayScheduleDTO staffSchedule)
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

        public static void GenerateStaffTotalContinuesFreeTimes(List<AppointmentDetailsDTO> appointments, StaffDayScheduleDTO staffSchedule, List<TimeOnly> times)
        {
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
        }
    }
}
