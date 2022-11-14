using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRoleRepository : IGenericRepository<MedicalStaffRole, Guid>
    {
        Task<MedicalStaffRole> InsertToMedicalStaffRole(Guid roleid, Guid medicalstaffid);
    }
}
