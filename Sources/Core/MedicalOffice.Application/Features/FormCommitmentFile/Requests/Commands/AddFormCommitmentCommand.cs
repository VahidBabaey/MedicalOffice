using MediatR;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands
{
    public class AddFormCommitmentCommand : IRequest<BaseResponse>
    {
        public AddFormCommitmentDTO DTO { get; set; } = new AddFormCommitmentDTO();
        public Guid OfficeId { get; set; }
    }
}
