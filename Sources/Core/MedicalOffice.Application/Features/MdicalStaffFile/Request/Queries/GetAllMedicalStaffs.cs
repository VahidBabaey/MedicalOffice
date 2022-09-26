using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MdicalStaffFile.Request.Queries
{
    public class GetAllMedicalStaffs : IRequest<List<MedicalStaffListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
    }
}
