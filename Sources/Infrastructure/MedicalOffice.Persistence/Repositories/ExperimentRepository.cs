using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ExperimentRepository : GenericRepository<Experiment, Guid>, IExperimentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExperimentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckExistExperimentId(Guid officeId, Guid experimentId)
    {
        bool isExist = await _dbContext.ExperimentPres.AnyAsync(p => p.OfficeId == officeId && p.Id == experimentId);
        return isExist;
    }
}
