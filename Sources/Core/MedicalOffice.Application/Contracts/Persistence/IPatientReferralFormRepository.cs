using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPatientReferralFormRepository : IGenericRepository<PatientReferralForm, Guid>
    {
        Task<bool> CheckExistPatientReferralFormId(Guid patientReferralFormId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId();
        Task<IReadOnlyList<PatientReferralForm>> GetByPatientId(Guid patientid);
    }
}
