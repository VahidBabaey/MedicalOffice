using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class ServiceRoom : BaseDomainEntity<Guid>
    {
        public Guid ServiceId{ get; set; }

        public Service Service { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }
    }
}
