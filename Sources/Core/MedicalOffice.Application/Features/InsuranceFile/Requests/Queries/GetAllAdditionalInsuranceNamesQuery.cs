using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Requests.Queries
{
    public class GetAllAdditionalInsuranceNamesQuery : IRequest<BaseResponse>
    {
        public InsuranceNamesDTO DTO { get; set; } = new InsuranceNamesDTO();
        public Guid OfficeId { get; set; }
    }
}
