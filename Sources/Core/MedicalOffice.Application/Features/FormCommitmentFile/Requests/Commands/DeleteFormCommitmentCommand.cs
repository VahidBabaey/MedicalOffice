using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands
{
    public class DeleteFormCommitmentCommand : IRequest<BaseResponse>
    {
        public Guid FormCommitmentID { get; set; }
        public Guid OfficeId { get; set; }
    }
}
