using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries
{
    public class GatAllFormCommitmentQuery : IRequest<List<FormCommitmentListDTO>>
    {
        public ListDto Dto { get; set; } = new ListDto();
    }
}
