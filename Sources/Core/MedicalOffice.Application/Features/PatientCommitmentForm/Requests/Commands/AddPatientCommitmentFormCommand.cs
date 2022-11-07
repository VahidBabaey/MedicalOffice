using MediatR;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command
{
    public class AddPatientCommitmentFormCommand : IRequest<BaseCommandResponse>
    {
        public AddPatientCommitmentsFormDTO DTO { get; set; } = new AddPatientCommitmentsFormDTO();
    }
}
