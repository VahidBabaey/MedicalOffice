using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries
{
    public class GetAllMedicalStaffScheduleQuery : IRequest<List<MedicalStaffScheduleListDTO>>
    {
        public Guid MedicalStaffId { get; set; }
    }
}
