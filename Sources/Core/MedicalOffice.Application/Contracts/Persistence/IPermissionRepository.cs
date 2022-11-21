using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id);

        Guid GetId(Guid id);

        Task<bool> UserHasPermission(Guid userId, Guid officeId, string[] permission);

        Task<IReadOnlyList<Permission>> GetPermissionDetailsByMedicalStaffID(Guid Id);

    }
}
