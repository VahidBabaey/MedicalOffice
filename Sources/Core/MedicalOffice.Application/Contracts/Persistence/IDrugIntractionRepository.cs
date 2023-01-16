using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugIntractionRepository : IGenericRepository<DrugIntraction, Guid>
    {
        Task<bool> CheckExistDrugId(Guid drugIntractionId);
        Task<bool> CheckExistDrugIntractionId(Guid drugIntractionId);
        Task<IEnumerable<DrugIntractionListDTO>> GetAllDrugIntractions();
    }
}
