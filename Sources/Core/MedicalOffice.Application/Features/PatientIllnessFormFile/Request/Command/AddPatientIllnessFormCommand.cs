using MediatR;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command
{
    public class AddPatientIllnessFormCommand : IRequest<BaseCommandResponse>
    {
        public PatientIllnessFormDTO DTO { get; set; } = new PatientIllnessFormDTO();
    }
}
