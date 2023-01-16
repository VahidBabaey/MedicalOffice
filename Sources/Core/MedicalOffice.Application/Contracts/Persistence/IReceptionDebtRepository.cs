using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IReceptionDebtRepository : IGenericRepository<ReceptionDebt, Guid>
    {
        Task<Guid> AddReceptionDebt(Guid receptionId, Guid receptionDetailId, Guid officeId, long receptionDebtPrice);
    }
}
