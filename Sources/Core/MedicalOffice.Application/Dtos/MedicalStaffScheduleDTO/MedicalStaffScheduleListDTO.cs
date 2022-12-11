using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO
{
    public class MedicalStaffScheduleListDTO
    {
        public Guid MedicalStaffId { get; set; }

        public int MaxAppointmentCount { get; set; }

        public TimeOnly MorningStart { get; set; } = TimeOnly.MinValue;

        public TimeOnly MorningEnd { get; set; } = TimeOnly.MinValue;

        public TimeOnly EveningStart { get; set; } = TimeOnly.MinValue;

        public TimeOnly EveningEnd { get; set; } = TimeOnly.MinValue;
    }
}
