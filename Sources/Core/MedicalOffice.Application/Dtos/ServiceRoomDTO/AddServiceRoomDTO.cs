using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO
{
    public class AddServiceRoomDTO : IServiceIdsDTO,INameDTO
    {
        public string Name { get; set; }

        public List<Guid> ServiceIds { get; set; }
    }
}