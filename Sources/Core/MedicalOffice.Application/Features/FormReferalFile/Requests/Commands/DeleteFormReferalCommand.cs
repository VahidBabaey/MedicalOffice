using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormReferalFile.Requests.Commands
{
    public class DeleteFormReferalCommand : IRequest<BaseResponse>
    {
        public Guid FormReferalID { get; set; }
        public Guid OfficeId { get; set; }
    }
}
