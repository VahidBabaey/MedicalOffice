using MediatR;
using MedicalOffice.Application.Dtos.Permission;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Commands
{
    public class UpdateMedicalStaffCommand : IRequest<BaseResponse>
    {
        public UpdateMedicalStaffPermissionsDTO DTO { get; set; } = new UpdateMedicalStaffPermissionsDTO();

        public Guid OffceId{ get; set; }
    }
}
