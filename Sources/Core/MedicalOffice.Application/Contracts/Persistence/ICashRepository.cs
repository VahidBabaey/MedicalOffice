using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashRepository : IGenericRepository<Cash, Guid>
    {
        Task<Guid> AddCashForAnyReceptionDetail(Guid OfficeId, Guid receptionId, long recieved);
        Task<bool> CheckExistCashId(Guid officeId, Guid cashId);
        Task<bool> CheckExistReceptionId(Guid officeId, Guid receptionId);
        Task<List<CashListDTO>> GetPatientCashes(Guid officeId, Guid receptionId);
        Task<Guid> ReturnCash(Guid officeId, Guid cashId);
    }
}
