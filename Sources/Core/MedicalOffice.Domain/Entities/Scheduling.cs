using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Scheduling : BaseDomainEntity<Guid>
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public List<string> PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public Guid MedicalStaffId { get; set; }

        public MedicalStaff MedicalStaff { get; set; }

        public Guid ServiceId { get; set; }

        public Service Service { get; set; }

        public Enums.AppointmentType Status { get; set; }

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;
    }
}
