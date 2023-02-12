using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Commands
{
    public class DeleteMembershipServiceCommand : IRequest<BaseResponse>
    {
        public Guid MembershipServiceId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
