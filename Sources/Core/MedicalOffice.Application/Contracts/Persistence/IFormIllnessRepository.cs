using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IFormIllnessRepository : IGenericRepository<FormIllness, Guid>
    {
        Task<bool> CheckExistFormIllnessId(Guid officeId, Guid formIllnessId);
        Task<bool> CheckExistFormIllnessName(Guid officeId, string formIllnessName);
    }
}
