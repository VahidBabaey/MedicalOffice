using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficeRoleRepository : IGenericRepository<UserOfficeRole, Guid>
    {
        Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid UserId, Guid officeId);

        Task<List<UserOfficeRole>> GetByUserId(Guid UserId);
    }
}
