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
    public class GetAllMedicalStaffsNamesandRolesQuery : IRequest<List<MedicalStaffNamesDTO>>
    {
        public MedicalStaffNamesDTO DTO { get; set; } = new MedicalStaffNamesDTO();
    }
}
