using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IBasicInfoDetailRepository : IGenericRepository<BasicInfoDetail, Guid>
    {
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoCommitmentId();
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId(Guid basicInfoId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoIllnessId();
    }
}
