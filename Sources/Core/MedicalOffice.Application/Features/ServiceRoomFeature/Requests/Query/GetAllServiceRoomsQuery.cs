using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Query
{
    public class GetAllServiceRoomsQuery : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}