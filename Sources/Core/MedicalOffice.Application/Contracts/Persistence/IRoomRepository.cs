using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IRoomRepository : IGenericRepository<Room, Guid>
    {
        Task<List<ServiceRoomListDTO>> GetServiceRooms(Guid officeId);
        Task<bool> isNameUniqe(string roomName, Guid officeId);
        Task<bool> isServiceRoomExist(Guid officeId, Guid serviceRoomId);
        Task<List<Guid>> SoftDeleteRange(List<Guid> serviceRoomIds);
        Task<Guid> UpdateRoomAndRoomServices(Room room, List<ServiceRoom> serviceRooms);
    }
}