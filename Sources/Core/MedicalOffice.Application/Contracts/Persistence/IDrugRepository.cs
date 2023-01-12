using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugRepository : IGenericRepository<Drug, Guid>
    {
        Task<bool> CheckExistDrugConsuptionId(Guid drugConsumptionId);
        Task<bool> CheckExistDrugId(Guid officeId, Guid drugId);
        Task<bool> CheckExistDrugSectionId(Guid drugSectionId);
        Task<bool> CheckExistDrugShapeId(Guid drugShapeId);
        Task<bool> CheckExistDrugUsageId(Guid drugUsageId);
        Task<IEnumerable<DrugListDTO>> GetAllDrugs(Guid officeId);
    }
}
