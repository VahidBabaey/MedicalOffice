using MediatR;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Queries
{
    public class GetCalculatedDiscountQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid MembershipId { get; set; }
    }
}
