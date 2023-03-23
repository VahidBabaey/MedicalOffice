using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Request.Commands
{
    public class DeleteCashCommand : IRequest<BaseResponse>
    {
        public Guid CashTypeId { get; set; }
        public Cashtype Cashtype { get; set; }
    }
}
