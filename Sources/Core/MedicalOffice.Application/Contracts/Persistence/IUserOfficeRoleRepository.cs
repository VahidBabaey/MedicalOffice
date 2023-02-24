using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficeRoleRepository :IGenericRepository<UserOfficeRole,Guid>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId,Guid OfficeId);

        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId, Guid officeId);

        Task AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles);

        Task<List<UserOfficeRole>> GetByUserId(Guid UserId);

        Task<UserOfficeRole> GetByUserAndOfficeId(Guid userId, Guid officeId);
    }
}