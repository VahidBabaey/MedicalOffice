using MediatR;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command
{
    public class DeleteServiceRoomCommand : IRequest<BaseResponse>
    {
        public ServiceRoomIdsDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}