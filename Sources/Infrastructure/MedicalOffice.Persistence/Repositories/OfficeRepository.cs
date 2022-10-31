using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class OfficeRepository : GenericRepository<Office, Guid>, IOfficeRepository
{
    private readonly ApplicationDbContext _dbcontext; 
    public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
    { 
        _dbcontext = dbContext; 
    }

    public async Task<IReadOnlyList<Office>> GetByUserId(Guid userId)
    {
        List<Office> offices = await _dbcontext.Offices.Include(s => s.UserOfficeRoles.Where(x => x.UserId == userId)).ToListAsync();
        return offices;
    }
}
