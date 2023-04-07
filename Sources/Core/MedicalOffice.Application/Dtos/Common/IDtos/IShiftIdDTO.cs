using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IDtos
{
    public interface IShiftIdDTO
    {
        public Guid ShiftId { get; set; }
    }
}