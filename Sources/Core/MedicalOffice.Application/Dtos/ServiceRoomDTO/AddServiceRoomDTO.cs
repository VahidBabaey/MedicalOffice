using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO
{
    public class AddServiceRoomDTO : IServiceIdsDTO,IRoomNameDTO
    {
        public string RoomName { get; set; }

        public List<Guid> ServiceIds { get; set; }
    }
}