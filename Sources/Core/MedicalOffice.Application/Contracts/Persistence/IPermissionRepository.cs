using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<IReadOnlyList<Permission>> GetPermissionDetailsByMedicalStaffID(Guid Id);
        Guid GetId(Guid id);
        Task<bool> SearchMedicalStaff(Guid searchid);
    }
}
