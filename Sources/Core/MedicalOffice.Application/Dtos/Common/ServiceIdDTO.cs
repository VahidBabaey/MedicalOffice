using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common
{
    public class ServiceIdDTO : IServiceIdDTO
    {
        public Guid ServiceId { get; set; }
    }
}
