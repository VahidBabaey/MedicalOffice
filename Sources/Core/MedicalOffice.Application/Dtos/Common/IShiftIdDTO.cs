using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Commons
{
    public interface IShiftIdDTO
    {
        public Guid ShiftId { get; set; }
    }
}