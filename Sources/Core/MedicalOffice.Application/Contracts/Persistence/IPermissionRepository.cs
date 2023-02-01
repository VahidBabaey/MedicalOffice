using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<bool> UserHasPermission(Guid userId, Guid officeId, string[] permission);
    }
}
