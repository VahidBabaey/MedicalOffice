using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashPosRepository : IGenericRepository<CashPos, Guid>
    {
        Task<Guid> AddCashPosForAnyReceptionDetail(Guid OfficeId, Guid receptionId, Guid cashid, long recieved, Guid bankid);
        Task<bool> CheckCashPosId(Guid cashPosId);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptonId);
    }
}
