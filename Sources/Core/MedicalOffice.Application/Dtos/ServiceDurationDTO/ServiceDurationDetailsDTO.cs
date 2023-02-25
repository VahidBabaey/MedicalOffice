using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceDurationDTO
{
    public class ServiceDurationDetailsDTO : BaseDto<Guid>
    {
        public Guid MedicalStaffId { get; set; }
        public string StaffName { get; set; }
        public Guid ServiceId{ get; set; }
        public string ServiceName { get; set; }
        public int Duration { get; set; }


    }
}
