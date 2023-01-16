using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceTariffRepository : IGenericRepository<Tariff, Guid>
    {
        Task<bool> CheckExistServiceId(Guid officeId, Guid serviceId);
    }
}
