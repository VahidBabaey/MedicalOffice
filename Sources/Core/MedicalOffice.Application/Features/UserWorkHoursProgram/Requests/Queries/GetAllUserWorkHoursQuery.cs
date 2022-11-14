using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Queries
{
    public class GetAllMedicalStaffWorkHoursQuery : IRequest<List<MedicalStaffWorkHoursProgramListDTO>>
    {
        public Guid MedicalStaffId { get; set; }
    }
}
