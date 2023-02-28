using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Room : BaseDomainEntity<Guid>
    {
        public Guid OfficeId { get; set; }

        public Office Office { get; set; }

        public string Name { get; set; }

        public ICollection<ServiceRoom> ServiceRooms { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}