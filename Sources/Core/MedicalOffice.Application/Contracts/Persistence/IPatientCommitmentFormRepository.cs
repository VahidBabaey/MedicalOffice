using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientCommitmentFormRepository : IGenericRepository<PatientCommitmentForm, Guid>
    {
        Task<bool> CheckExistPatientCommitmentFormId(Guid patientCommitmentFormId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId();
        Task<IReadOnlyList<PatientCommitmentForm>> GetByPatientId(Guid patientid);
    }
}
