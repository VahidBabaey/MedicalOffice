using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Queries
{
    public class GetAllPatientReferralFormQuery : IRequest<List<PatientReferralFormListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid PatientId { get; set; }
    }
}
