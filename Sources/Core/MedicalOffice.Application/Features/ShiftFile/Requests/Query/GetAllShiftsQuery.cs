using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Shift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Requests.Query
{
    public class GetAllShiftsQuery : IRequest<List<ShiftListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}
