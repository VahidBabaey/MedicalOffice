using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id);
        Guid GetId(Guid id);
        Task<bool> SearchUser(Guid searchid);
    }
}
