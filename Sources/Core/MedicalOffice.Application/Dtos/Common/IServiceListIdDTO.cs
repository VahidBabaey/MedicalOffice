using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Commons
{
    public interface IServiceListIdDTO
    {
        public Guid[]? ServiceId { get; set; }
    }
}

