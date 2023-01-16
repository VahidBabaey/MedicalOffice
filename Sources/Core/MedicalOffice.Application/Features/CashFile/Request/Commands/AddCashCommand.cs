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
    public class AddCashCommand : IRequest<BaseResponse>
    {
        public CashesDTO DTO { get; set; } = new CashesDTO();
        public Guid OfficeId { get; set; }
    }
}
