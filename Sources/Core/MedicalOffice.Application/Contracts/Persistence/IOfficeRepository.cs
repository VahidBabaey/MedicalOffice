using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IOfficeRepository : IGenericRepository<Office, Guid>
    {
        Task<bool> CheckExistOfficeId(Guid officeId);
        Task<List<Office?>> GetByUserId(Guid userId);
    }
}
