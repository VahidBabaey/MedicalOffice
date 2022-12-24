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
    public class GetPatientCashesQuery : IRequest<List<CashListDTO>>
    {
        //public ListDto Dto { get; set; } = new ListDto();
        public Guid ReceptionId { get; set; }
    }
}
