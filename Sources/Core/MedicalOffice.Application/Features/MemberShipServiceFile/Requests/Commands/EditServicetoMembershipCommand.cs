using MediatR;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands
{
    public class EditServicetoMembershipCommand : IRequest<BaseResponse>
    {
        public UpdateMemberShipServiceDTO DTO { get; set; } = new UpdateMemberShipServiceDTO();
        public Guid OfficeId { get; set; }
    }
}
