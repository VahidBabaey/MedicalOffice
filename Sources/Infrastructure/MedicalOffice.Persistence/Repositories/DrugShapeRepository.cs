using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugShapeRepository : GenericRepository<DrugShape, Guid>, IDrugShapeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DrugShapeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
