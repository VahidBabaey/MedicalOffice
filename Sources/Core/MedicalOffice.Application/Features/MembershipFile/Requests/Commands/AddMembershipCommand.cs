using MediatR;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Requests.Commands
{
    public class AddMembershipCommand : IRequest<BaseResponse>
    {
        public MembershipDTO DTO { get; set; } = new MembershipDTO();
        public Guid OfficeId { get; set; }
    }
}
