using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetAllPatientsByGenderIntrucerAcquaintedWayQuery : IRequest<List<PatientListDto>>
    {
        public int gender { get; set; }
        public int intrucer { get; set; }
        public int acquaintedway { get; set; }
        public ListDto Dto { get; set; } = new ListDto();
    }
}
