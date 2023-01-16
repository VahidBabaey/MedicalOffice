using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Requests.Commands
{
    public class DeleteMembershipCommand : IRequest<BaseResponse>
    {
        public Guid MembershipId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
