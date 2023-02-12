using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries
{
    public class GetAllServicesOfMemberShipQueryBySearch : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();
        public Guid MemberShipId { get; set; }
        public Guid OfficeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
