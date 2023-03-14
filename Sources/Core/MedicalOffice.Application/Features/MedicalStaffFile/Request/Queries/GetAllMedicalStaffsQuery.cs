using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries
{
    public class GetAllMedicalStaffsQuery : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
        public Order? Order { get;}
    }
}
