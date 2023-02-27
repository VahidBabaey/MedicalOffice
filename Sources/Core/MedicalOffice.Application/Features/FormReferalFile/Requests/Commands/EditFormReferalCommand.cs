using MediatR;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormReferalDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormReferalFile.Requests.Commands
{
    public class EditFormReferalCommand : IRequest<BaseResponse>
    {
        public UpdateFormReferalDTO DTO { get; set; } = new UpdateFormReferalDTO();
        public Guid OfficeId { get; set; }
    }
}
