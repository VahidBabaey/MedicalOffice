using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses.Enveloping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Office.Requests.Queries
{
    public class GetByUserIdQuery : IRequest<BaseQueryResponse>
    {
        public OfficesByUserIdDTO Dto { get; set; } = new OfficesByUserIdDTO();
    }
}
