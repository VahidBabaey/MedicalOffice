using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugUsageRepository : IGenericRepository<DrugUsage, Guid>
    {

    }
}
