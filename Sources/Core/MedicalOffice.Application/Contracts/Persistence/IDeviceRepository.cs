using MedicalOffice.Domain;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDeviceRepository : IGenericRepository<Device, Guid>
    {
    }
}