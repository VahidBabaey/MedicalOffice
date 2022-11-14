using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Queries
{
    public class GetAllAdditionalInsuranceNamesQuery : IRequest<List<InsuranceNamesDTO>>
    {
        public InsuranceNamesDTO DTO { get; set; } = new InsuranceNamesDTO();
    }
}
