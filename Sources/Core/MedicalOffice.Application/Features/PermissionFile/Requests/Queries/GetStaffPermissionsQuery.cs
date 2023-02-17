using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Queries
{
    public class GetStaffPermissionsQuery : IRequest<BaseResponse>
    {
        public Guid StaffId { get; set; }
        public Guid OfficeId { get; set; }
    }
}