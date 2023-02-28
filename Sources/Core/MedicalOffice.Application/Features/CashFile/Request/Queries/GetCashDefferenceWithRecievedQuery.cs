using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Request.Queries
{
    public class GetCashDefferenceWithRecievedQuery : IRequest<BaseResponse>
    {
        public CashDifferenceWithRecieved DTO { get; set; } = new CashDifferenceWithRecieved();
        public Guid ReceptionId { get; set; }
    }
}
