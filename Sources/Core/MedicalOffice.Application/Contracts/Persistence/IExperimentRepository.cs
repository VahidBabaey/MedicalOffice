using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IExperimentRepository : IGenericRepository<Experiment, Guid>
    {
        Task<bool> CheckExistExperimentId(Guid officeId, Guid experimentId);
        Task<List<Experiment>> GetExperimentBySearch(string name);
    }
}
