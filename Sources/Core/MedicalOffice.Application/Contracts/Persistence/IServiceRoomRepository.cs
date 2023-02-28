using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceRoomRepository : IGenericRepository<ServiceRoom, Guid>
    {
        Task SoftDeleteRange(Guid roomId);
    }
}