using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class SpecializationRepository : GenericRepository<Specialization, Guid>, ISpecializationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpecializationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}
