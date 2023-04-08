
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO
{
    public class UpdateServiceRoomDTO : BaseDto<Guid>, IServiceIdsDTO, INameDTO
    {
        public string Name { get; set; }

        public List<Guid> ServiceIds { get; set; }
    }
}