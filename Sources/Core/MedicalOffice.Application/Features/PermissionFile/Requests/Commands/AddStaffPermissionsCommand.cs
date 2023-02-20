using MediatR;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Commands
{
    public class AddStaffPermissionsCommand : IRequest<BaseResponse>
    {
        public List<PermissionDto> DTO { get; set; }
        public Guid OfficeId { get; set; }
        public Guid StaffId { get; set; }
    }
}