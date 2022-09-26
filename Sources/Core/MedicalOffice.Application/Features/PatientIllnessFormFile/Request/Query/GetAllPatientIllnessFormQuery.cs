using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query
{
    public class GetAllPatientIllnessFormQuery : IRequest<List<PatientIllnessFormListDTO>>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid PatientId { get; set; }
    }
}
