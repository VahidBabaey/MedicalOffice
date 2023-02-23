using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries
{
    public class GetAllMedicalStaffScheduleQuery : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; }
        public Guid MedicalStaffId { get; set; }

        public Guid OfficeId{ get; set; }
    }
}
