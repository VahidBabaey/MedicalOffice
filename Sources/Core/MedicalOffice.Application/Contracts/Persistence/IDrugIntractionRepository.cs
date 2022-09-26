using MedicalOffice.Application.Dtos.DrugIntractionD;
using MedicalOffice.Application.Dtos.DrugPreDrugIntraction;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugIntractionRepository : IGenericRepository<DrugIntraction, Guid>
    {
        
        Task<IEnumerable<DrugIntractionListDTO>> GetAllDrugIntractions();
    }
}
