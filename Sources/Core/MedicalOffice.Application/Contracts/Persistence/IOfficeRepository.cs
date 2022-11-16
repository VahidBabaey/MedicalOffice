using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IOfficeRepository : IGenericRepository<Office, Guid>
    {
        Task<List<Office?>> GetByUserId(Guid userId);
    }
}
