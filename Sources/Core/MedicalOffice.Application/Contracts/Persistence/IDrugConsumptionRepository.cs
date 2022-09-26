using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IDrugConsumptionRepository : IGenericRepository<DrugConsumption, Guid>
    {

    }
}
