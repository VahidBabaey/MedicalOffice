using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.RoleFile.Requests.Queries
{
    public class GetRoleSituationQuery : IRequest<BaseResponse>
    {
        public Guid RoleId { get; set; }
    }
}