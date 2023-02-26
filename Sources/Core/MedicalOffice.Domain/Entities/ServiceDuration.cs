using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class ServiceDuration : BaseDomainEntity<Guid>
    {
        public Guid OfficeId { get; set; }

        public Office Office{ get; set; }

        public Guid MedicalStaffId { get; set; }

        public MedicalStaff MedicalStaff { get; set; }

        public Guid ServiceId { get; set; }

        public Service Service { get; set; }

        public int Duration { get; set; }
    }
}
