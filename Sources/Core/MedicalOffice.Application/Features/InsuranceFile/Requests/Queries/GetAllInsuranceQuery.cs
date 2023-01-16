using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Requests.Queries
{
    public class GetAllInsuranceQuery : IRequest<List<InsuranceListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
    }
}
