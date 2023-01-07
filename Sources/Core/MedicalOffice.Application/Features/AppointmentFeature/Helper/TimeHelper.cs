using MedicalOffice.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Helper
{
    public static class TimeHelper
    {
        public static bool isTimeValid(AppointmentDetailsDTO time, string startTime, string endTime)
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
