using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command
{
    public class DeletePatientCommitmentFormCommand : IRequest<BaseCommandResponse>
    {
        public Guid PatientCommitmentFormId { get; set; }
    }
}
