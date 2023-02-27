using MediatR;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormIllnessDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormIllnessFile.Requests.Commands
{
    public class AddFormIllnessCommand : IRequest<BaseResponse>
    {
        public AddFormIllnessDTO DTO { get; set; } = new AddFormIllnessDTO();
        public Guid OfficeId { get; set; }
    }
}
