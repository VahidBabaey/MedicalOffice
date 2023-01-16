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
    public class EditCashCheckCommand : IRequest<BaseResponse>
    {
        public UpdateCashCheckDTO DTO { get; set; } = new UpdateCashCheckDTO();
        public Guid OfficeId { get; set; }
    }
}
