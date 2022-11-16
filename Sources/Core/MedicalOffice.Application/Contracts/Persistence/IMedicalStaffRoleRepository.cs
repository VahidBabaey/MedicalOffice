using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffRoleRepository : IGenericRepository<MedicalStaffRole, Guid>
    {
        Task InsertToMedicalStaffRole(List<MedicalStaffRole> medicalStaffRole);
    }
}
