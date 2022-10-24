using MediatR;
using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserFile.Request.Commands
{
    public class EditUserCommand : IRequest<BaseCommandResponse>
    {
        public UpdateMedicalStaffDTO DTO { get; set; } = new UpdateMedicalStaffDTO();
    }
}
