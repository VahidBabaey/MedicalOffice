using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface ICashCheckRepository : IGenericRepository<CashCheck, Guid>
    {

    }
}
