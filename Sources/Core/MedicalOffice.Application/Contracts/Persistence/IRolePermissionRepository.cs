using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IRolePermissionRepository : IGenericRelationalEntitiesRepository<RolePermission>
    {
        Task<List<Permission>> GetByRoleId(Guid roleId);
    }
}