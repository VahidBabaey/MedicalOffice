using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries
{
    public class GetMedicalStaffBySearch : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();
        public string Name { get; set; }
        public Guid OfficeId { get; set; }
    }
}
