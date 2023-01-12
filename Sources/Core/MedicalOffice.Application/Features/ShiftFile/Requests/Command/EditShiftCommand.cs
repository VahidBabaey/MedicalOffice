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
    public class EditShiftCommand : IRequest<BaseResponse>
    {
        public UpdateShiftDTO DTO { get; set; } = new UpdateShiftDTO();
        public Guid OfficeId { get; set; }
    }
}
