using MediatR;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Requests.Command
{
    public class AddShiftCommand : IRequest<BaseResponse>
    {
        public ShiftDTO DTO { get; set; } = new ShiftDTO();
        public Guid OfficeId { get; set; }
    }
}
