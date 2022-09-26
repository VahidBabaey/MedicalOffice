using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugUsageRepository : GenericRepository<DrugUsage, Guid>, IDrugUsageRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DrugUsageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
