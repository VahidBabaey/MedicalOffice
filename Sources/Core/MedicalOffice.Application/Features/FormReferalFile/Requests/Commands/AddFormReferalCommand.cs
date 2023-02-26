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
    public class AddFormReferalCommand : IRequest<BaseResponse>
    {
        public AddFormReferalDTO DTO { get; set; } = new AddFormReferalDTO();
        public Guid OfficeId { get; set; }
    }
}
