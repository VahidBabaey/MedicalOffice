using MediatR;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Commands
{
    public class DeleteReceptionDetailCommand : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public Guid ReceptiodDetailId { get; set; }
    }
}
