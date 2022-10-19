using MediatR;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Commands
{
    public class EditPermissionCommand : IRequest<BaseCommandResponse>
    {
        public UpdatePermissionDTO DTOUp { get; set; } = new UpdatePermissionDTO();
    }
}
