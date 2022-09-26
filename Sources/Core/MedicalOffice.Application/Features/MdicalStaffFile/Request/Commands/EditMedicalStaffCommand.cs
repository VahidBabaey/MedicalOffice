using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MdicalStaffFile.Request.Commands
{
    public class EditMedicalStaffCommand : IRequest<BaseCommandResponse>
    {
        public MedicalStaffDTO DTO { get; set; } = new MedicalStaffDTO();
    }
}
