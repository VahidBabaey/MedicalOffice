using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ShiftDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Requests.Query
{
    public class GetShiftBySearchQuery : IRequest<List<ShiftListDTO>>
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
    }
}
