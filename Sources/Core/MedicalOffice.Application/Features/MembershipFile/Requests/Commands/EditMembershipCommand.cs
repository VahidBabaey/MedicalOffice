using MediatR;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Requests.Commands
{
    public class EditMembershipCommand : IRequest<BaseCommandResponse>
    {
        public MembershipDTO DTO { get; set; } = new MembershipDTO();
    }
    
    
}
