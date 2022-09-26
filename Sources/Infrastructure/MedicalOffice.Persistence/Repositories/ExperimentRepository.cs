using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class ExperimentRepository : GenericRepository<ExperimentPre, Guid>, IExperimentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExperimentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
