using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientContactRepository : IGenericRepository<PatientContact, Guid>
    {
        Task<bool> RemovePatientContact(Guid patientId);
    }
}
