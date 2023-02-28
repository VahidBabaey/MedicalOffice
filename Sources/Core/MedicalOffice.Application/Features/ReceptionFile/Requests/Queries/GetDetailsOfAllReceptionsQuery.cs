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
    public class GetDetailsOfAllReceptionsQuery : IRequest<BaseResponse>
    {
        public Guid PatientId { get; set; }
        public Guid ReceptionId { get; set; }
    }
}
