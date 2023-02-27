using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormIllnessFile.Requests.Commands
{
    public class DeleteFormIllnessCommand : IRequest<BaseResponse>
    {
        public Guid FormIllnessID { get; set; }
        public Guid OfficeId { get; set; }
    }
}
