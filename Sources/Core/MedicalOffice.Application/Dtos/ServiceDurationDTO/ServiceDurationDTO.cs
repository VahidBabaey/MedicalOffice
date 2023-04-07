using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceDurationDTO
{
    public class ServiceDurationDTO:IMedicalStaffDTO,IServiceIdDTO
    {
        public Guid MedicalStaffId{ get; set; }
        
        public Guid ServiceId{ get; set; }
        
        public int Duration{ get; set; }
    }
}
