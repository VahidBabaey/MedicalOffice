using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceRepository : IGenericRepository<Service, Guid>
    {
        Task<IReadOnlyList<Service>> GetBySectionId(Guid sectionId);
        Task<IReadOnlyList<Service>> GetServiceByID(Guid sectionId);
    }
}
