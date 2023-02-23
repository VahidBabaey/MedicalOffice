using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ISectionRepository : IGenericRepository<Section, Guid>
    {
        Task<bool> CheckExistSectionId(Guid officeId, Guid sectionId);
        Task<bool> CheckExistSectionName(Guid officeId, string sectionName);
        Task<List<Section>> GetSectionBySearch(string name, Guid officeId);
        Task<List<SectionNamesListDTO>> GetSectionNames(Guid officeId);
    }
}
