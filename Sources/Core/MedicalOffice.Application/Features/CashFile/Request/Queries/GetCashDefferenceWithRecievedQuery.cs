using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Request.Queries
{
    public class GetCashDefferenceWithRecievedQuery : IRequest<CashDifferenceWithRecieved>
    {
        public CashDifferenceWithRecieved DTO { get; set; } = new CashDifferenceWithRecieved();
        public Guid ReceptionId { get; set; }
    }
}
