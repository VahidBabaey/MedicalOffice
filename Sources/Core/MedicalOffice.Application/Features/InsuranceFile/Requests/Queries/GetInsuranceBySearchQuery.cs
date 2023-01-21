using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Requests.Queries
{
    public class GetInsuranceBySearchQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
    }
}
