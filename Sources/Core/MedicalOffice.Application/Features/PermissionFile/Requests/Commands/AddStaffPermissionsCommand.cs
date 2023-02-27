using MediatR;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Commands
{
    public class UpdateStaffPermissionsCommand : IRequest<BaseResponse>
    {
        public UpdateStaffPermissionDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}