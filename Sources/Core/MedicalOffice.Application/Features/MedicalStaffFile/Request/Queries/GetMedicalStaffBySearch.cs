using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries
{
    public class GetMedicalStaffBySearch : IRequest<List<MedicalStaffListDTO>>
    {
        public string Name { get; set; }
        public Guid OfficeId { get; set; }
    }
}
