using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashCartRepository : IGenericRepository<CashCart, Guid>
    {
        Task<Guid> AddCashCartForAnyReceptionDetail(Guid OfficeId, Guid receptionId, string cartnumber, long recieved, Guid bankid);
        Task<bool> CheckCashCartId(Guid cashCartId);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId);
        Task DeleteCashCartForAnyReceptionDetail(Guid checkId);
    }
}
