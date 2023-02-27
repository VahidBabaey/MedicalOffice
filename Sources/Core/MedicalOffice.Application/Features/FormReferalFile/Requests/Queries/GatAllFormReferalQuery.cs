using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormReferalFile.Requests.Queries
{
    public class GatAllFormReferalQuery : IRequest<BaseResponse>
    {
        public ListDto Dto { get; set; } = new ListDto();

        public Guid OfficeId { get; set; }
    }
}
