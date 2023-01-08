using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class CheckTimeResponseDTO
    {
        public string Message { get; set; }

        public bool IsValid { get; set; } = false;
    }
}
