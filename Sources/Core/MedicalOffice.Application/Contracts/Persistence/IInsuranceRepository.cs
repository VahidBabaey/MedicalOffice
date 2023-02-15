using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IInsuranceRepository : IGenericRepository<Insurance, Guid>
    {
        Task<bool> CheckExistInsuranceId(Guid officeId, Guid insuranceId);
        Task<IReadOnlyList<Insurance>> GetAllAdditionalInsuranceNames();
        Task<List<Insurance>> GetInsuranceBySearch(string name, int take, int skip);
        Task<List<InsuranceNamesDTO>> GetInsuranceNames(Guid officeId);
    }
}
