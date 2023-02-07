using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficeRoleRepository :IGenericJointEntitiesRepository<UserOfficeRole>
    {
        Task DeleteUserOfficeRoleAsync(Guid UserId,Guid OfficeId);

        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId, Guid officeId);

        Task AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles);

        Task<List<UserOfficeRole>> GetByUserId(Guid UserId);

        Task<List<UserOfficeRole>> GetByUserAndOfficeId(Guid userId, Guid officeId);
    }
}
