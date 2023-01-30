using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands
{
    public class EditMedicalStaffCommand : IRequest<BaseResponse>
    {
        public UpdateMedicalStaffDTO DTO { get; set; } = new UpdateMedicalStaffDTO();
        public Guid OfficeId { get; set; }
    }
}
