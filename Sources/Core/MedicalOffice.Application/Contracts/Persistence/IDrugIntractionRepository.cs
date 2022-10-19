using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugIntractionRepository : IGenericRepository<DrugIntraction, Guid>
    {
        
        Task<IEnumerable<DrugIntractionListDTO>> GetAllDrugIntractions();
    }
}
