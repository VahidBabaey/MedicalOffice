using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetPatientBySearchQuery : IRequest<BaseResponse> 
    {
        public ListDto Dto { get; set; } = new ListDto();
        public Guid OfficeId { get; set; }
        public SearchFields searchFields { get; set; } = new SearchFields();
        public PatientListDTO SearchDTO { get; set; } = new PatientListDTO();
    }
    public class SearchFields
    {
        public string NationalID { get; set; } = string.Empty;
        public int FileNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
