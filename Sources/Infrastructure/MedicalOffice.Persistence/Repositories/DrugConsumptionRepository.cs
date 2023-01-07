using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugConsumptionRepository : GenericRepository<DrugConsumption, Guid>, IDrugConsumptionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DrugConsumptionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
