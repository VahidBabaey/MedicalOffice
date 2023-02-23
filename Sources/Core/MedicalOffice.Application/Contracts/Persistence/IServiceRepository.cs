using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceRepository : IGenericRepository<Service, Guid>
    {
        Task<IReadOnlyList<Service>> GetBySectionId(Guid sectionId);

        Task<IReadOnlyList<Service>> GetServiceByID(Guid sectionId);

        Task<bool> CheckExistServiceListId(Guid officeId, Guid[] serviceId);

        Task<bool> CheckExistServiceId(Guid officeId, Guid serviceId);

        Task<List<Service>> GetServiceBySearch(string name, Guid sectionId, Guid officeId);

        Task<List<Service>> GetAllByOfficeId(Guid officeId);
        Task<bool> CheckExistServiceName(Guid officeId, string serviceName);
    }
}
