using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO
{
    public class ServiceRoomListDTO : BaseDto<Guid>
    {
        public string RoomName { get; set; }

        public List<ServiceIdNameDTO> ServiceIdNames{ get; set; }
    }
}