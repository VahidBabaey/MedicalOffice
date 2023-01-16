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
    public class AddCashPosCommand : IRequest<BaseResponse>
    {
        public CashPosDTO DTO { get; set; } = new CashPosDTO();
        public Guid OfficeId { get; set; }
    }
}
