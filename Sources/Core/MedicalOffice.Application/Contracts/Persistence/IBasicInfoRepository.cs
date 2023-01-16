using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IBasicInfoRepository : IGenericRepository<BasicInfo, Guid>
    {
        Task<bool> CheckExistBasicInfoId(Guid officeId, Guid basicInfoId);
    }
}
