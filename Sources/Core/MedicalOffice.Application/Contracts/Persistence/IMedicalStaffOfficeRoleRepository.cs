using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffOfficeRoleRepository : IGenericRepository<MedicalStaffOfficeRole, Guid>
    {
        Task<MedicalStaffOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId, Guid officeId);

        Task<List<MedicalStaffOfficeRole>> GetByUserId(Guid UserId);
    }
}
