using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceTariffRepository : IGenericRepository<Tariff, Guid>
    {
        Task<bool> CheckExistServiceId(Guid officeId, Guid serviceId);
        Task<bool> CheckExistTariffId(Guid officeId, Guid tariffId);
        Task<List<TariffListDTO>> GetTariffsofService(int skip, int take, Guid officeId, Guid serviceId);
    }
}
