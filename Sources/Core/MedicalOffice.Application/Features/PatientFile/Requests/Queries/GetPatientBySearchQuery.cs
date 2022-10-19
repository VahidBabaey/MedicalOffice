using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetPatientBySearchQuery : IRequest<List<PatientListDto>>
    {
        public ListDto Dto { get; set; } = new ListDto();
        public string nationalcode { get; set; } = string.Empty;
        public string filenumber { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;
        public string phonenumber { get; set; } = string.Empty;

    }
}
