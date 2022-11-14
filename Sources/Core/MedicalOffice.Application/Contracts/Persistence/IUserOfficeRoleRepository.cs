using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficeRoleRepository : IGenericRepository<UserOfficeRole, Guid>
    {
        Task<UserOfficeRole> InsertToUserOfficeRole(Guid userid, Guid officeid, Guid roleid);
    }
}
