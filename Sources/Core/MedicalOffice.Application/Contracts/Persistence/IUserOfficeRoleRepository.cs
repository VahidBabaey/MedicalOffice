using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficeRoleRepository : IGenericRepository<UserOfficeRole, Guid>
    {
        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId, Guid? officeId = null);

        Task<bool> AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles);

        Task<List<UserOfficeRole>> GetByUserId(Guid UserId);
    }
}
