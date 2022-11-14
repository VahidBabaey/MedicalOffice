using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientTagRepository : IGenericRepository<PatientTag, Guid>
    {
        Task<bool> RemovePatientTag(Guid patientId);
    }
}
