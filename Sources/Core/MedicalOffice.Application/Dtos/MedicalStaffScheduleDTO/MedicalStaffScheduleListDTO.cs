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

        public string MorningStart { get; set; }

        public string MorningEnd { get; set; }

        public string EveningStart { get; set; }

        public string EveningEnd { get; set; }
    }
}
