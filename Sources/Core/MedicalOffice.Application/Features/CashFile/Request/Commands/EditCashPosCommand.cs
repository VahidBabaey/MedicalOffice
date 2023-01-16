using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Request.Commands
{
    public class EditCashPosCommand : IRequest<BaseResponse>
    {
        public UpdateCashPosDTO DTO { get; set; } = new UpdateCashPosDTO();
        public Guid OfficeId { get; set; }
    }
}
