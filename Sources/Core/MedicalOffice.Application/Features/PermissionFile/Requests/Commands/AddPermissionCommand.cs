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
    public class AddPermissionCommand : IRequest<BaseResponse>
    {
        public string userid { get; set; } = string.Empty;
        public PermissionDTO DTO { get; set; } = new PermissionDTO();
        public UpdatePermissionDTO DTOUp { get; set; } = new UpdatePermissionDTO();
    }
}
