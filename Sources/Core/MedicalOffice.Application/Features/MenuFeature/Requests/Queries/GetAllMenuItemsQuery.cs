using MediatR;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.MenuFeature.Requests.Queries
{
    public class GetAllMenuItemsQuery : IRequest<BaseResponse>
    {
        public MenuDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}