using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<List<Permission>> GetByParentIds(List<Guid?> parentId);
        Task<bool> UserHasPermission(Guid userId, Guid officeId, string[] permission);
    }
}
