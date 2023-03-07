using MediatR;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries
{
    public class GetAllServicesQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public Order? Order { get; set; }
    }
}
