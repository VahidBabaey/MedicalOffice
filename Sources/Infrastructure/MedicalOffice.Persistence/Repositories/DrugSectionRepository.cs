using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class DrugSectionRepository : GenericRepository<DrugSection, Guid>, IDrugSectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DrugSectionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
