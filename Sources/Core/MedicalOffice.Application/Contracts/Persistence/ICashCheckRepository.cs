using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashCheckRepository : IGenericRepository<CashCheck, Guid>
    {
        Task<Guid> AddCashCheckForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved, Guid bankid);
        Task<bool> CheckCashCheckId(Guid cashCheckId);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId);
    }
}
