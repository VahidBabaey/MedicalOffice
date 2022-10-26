using MediatR;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands
{
    public class AddServicetoMembershipCommand : IRequest<BaseCommandResponse>
    {
        public MemberShipServiceDTO DTO { get; set; } = new MemberShipServiceDTO();
    }
}
