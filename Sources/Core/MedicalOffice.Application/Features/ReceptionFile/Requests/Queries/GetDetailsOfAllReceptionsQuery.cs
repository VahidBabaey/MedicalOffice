using MediatR;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.Reception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Queries
{
    public class GetDetailsOfAllReceptionsQuery : IRequest<DetailsofAllReceptionsDTO>
    {
        public Guid PatientId { get; set; }
    }
}
