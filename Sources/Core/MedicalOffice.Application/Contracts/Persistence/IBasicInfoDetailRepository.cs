using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IBasicInfoDetailRepository : IGenericRepository<BasicInfoDetail, Guid>
    {
        Task<bool> CheckExistBasicInfoDetailId(Guid basicInfoDetailId);
        Task<bool> CheckExistBasicInfoDetailName(string basicinfodetailName, Guid BasicInfoId);
        Task<bool> CheckExistBasicInfoId(Guid officeId, Guid basicInfoId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoCommitmentId();
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoId(Guid basicInfoId);
        Task<IReadOnlyList<BasicInfoDetail>> GetByBasicInfoIllnessId();
    }
}
