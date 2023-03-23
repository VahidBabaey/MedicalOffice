using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashCheckRepository : IGenericRepository<CashCheck, Guid>
    {
        Task<Guid> AddCashCheckForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved, Guid bankid, string branch);
        Task<bool> CheckCashCheckId(Guid cashCheckId);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId);
        Task DeleteCashCheckForAnyReceptionDetail(Guid checkId);
    }
}
