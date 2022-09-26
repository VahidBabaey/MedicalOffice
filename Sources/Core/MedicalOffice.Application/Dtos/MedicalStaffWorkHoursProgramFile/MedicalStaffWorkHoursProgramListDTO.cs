using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile
{
    public class MedicalStaffWorkHoursProgramListDTO
    {
        public Guid MedicalStaffId { get; set; }
        public int MaxAppointmentCount { get; set; }
        public string MorningStart { get; set; } = string.Empty;
        public string MorningEnd { get; set; } = string.Empty;
        public string EveningStart { get; set; } = string.Empty;
        public string EveningEnd { get; set; } = string.Empty;
    }
}
