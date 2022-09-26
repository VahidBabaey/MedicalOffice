using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IAccessRepository : IGenericRepository<Access, Guid>
    {
        Task<IReadOnlyList<Access>> GetAccessDetailsByUserID(Guid Id);
        string GetId(Guid id);
        bool SearchUser(Guid searchid);
    }
}
