using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Requests.Queries
{
    public class GetAllMemberships : IRequest<List<MembershipListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
    }
}
