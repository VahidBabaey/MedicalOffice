using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IInsuranceRepository : IGenericRepository<Insurance, Guid>
    {
        Task<bool> CheckExistInsuranceId(Guid insuranceId, Guid officeId);
        Task<IReadOnlyList<Insurance>> GetAllAdditionalInsuranceNames();
    }
}
