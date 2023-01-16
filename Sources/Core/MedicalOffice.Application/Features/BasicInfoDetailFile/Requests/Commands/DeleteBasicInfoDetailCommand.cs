using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands
{
    public class DeleteBasicInfoDetailCommand : IRequest<BaseResponse>
    {
        public Guid BasicInfoDetailId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
