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

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Queries
{
    public class GetByUserIdQuery : IRequest<BaseQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}
