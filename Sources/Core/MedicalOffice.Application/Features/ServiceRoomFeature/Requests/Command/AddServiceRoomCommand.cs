using MediatR;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command
{
    public class AddServiceRoomCommand : IRequest<BaseResponse>
    {
        public AddServiceRoomDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}