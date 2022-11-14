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
    public class AddMedicalStaffCommand : IRequest<BaseCommandResponse>
    {
        public MedicalStaffDTO DTO { get; set; } = new MedicalStaffDTO();
    }
}
