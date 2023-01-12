using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ISectionRepository : IGenericRepository<Section, Guid>
    {
        Task<bool> CheckExistSectionId(Guid officeId, Guid sectionId);
        Task<List<SectionListDTO>> GetAllSectionsByOfficeId(int skip, int take, Guid officeId);
    }
}
