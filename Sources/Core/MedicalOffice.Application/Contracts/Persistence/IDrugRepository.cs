using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugRepository : IGenericRepository<Drug, Guid>
    {
        Task<IEnumerable<DrugListDTO>> GetAllDrugs();
    }
}
