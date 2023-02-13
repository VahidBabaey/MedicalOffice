using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Queries
{
    public class GetPermissionsQuery : IRequest<BaseResponse>
    {
    }
}