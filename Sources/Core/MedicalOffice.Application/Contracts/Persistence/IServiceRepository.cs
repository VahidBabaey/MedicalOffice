using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceRepository : IGenericRepository<Service, Guid>
    {
        Task<IReadOnlyList<Service>> GetBySectionId(Guid sectionId);

        Task<IReadOnlyList<Service>> GetServiceByID(Guid sectionId);

        Task<bool> checkServiceExist(Guid serviceId,Guid officeId);
        Task<bool> CheckExistServiceListId(Guid officeId, Guid[] serviceId);
    }
}
