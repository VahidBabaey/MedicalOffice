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
    public class GetPatientBySearchQuery : IRequest<List<PatientListDTO>> 
    {
        public ListDto Dto { get; set; } = new ListDto();
        public SearchFields searchFields { get; set; } = new SearchFields();
        public PatientListDTO SearchDTO { get; set; } = new PatientListDTO();
    }
    public class SearchFields
    {
        public string NationalID { get; set; } = string.Empty;
        public string FileNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
