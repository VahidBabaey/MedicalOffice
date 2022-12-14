using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Room:BaseDomainEntity<Guid>
    {
        public string Name{ get; set; }

        public Guid DeviceId{ get; set; }

        public Device Device{ get; set; }
    }
}
