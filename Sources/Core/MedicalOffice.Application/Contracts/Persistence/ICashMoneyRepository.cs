using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashMoneyRepository : IGenericRepository<CashMoney, Guid>
    {
        Task<Guid> AddCashMoneyForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved);
        Task<bool> CheckCashMoneyId(Guid cashMoneyId);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId);
    }
}
