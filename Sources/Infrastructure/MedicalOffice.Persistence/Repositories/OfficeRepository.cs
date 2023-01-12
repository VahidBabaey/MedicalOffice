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

    public async Task<List<Office?>> GetByUserId(Guid userId)
    {
        var offices = await _dbcontext.UserOfficeRoles.Include(uor => uor.Office).Where(u => u.UserId == userId)
            .Select(uor => uor.Office).ToListAsync();

        return offices;
    }
    public async Task<bool> CheckExistOfficeId(Guid officeId)
    {
        bool isExist = await _dbcontext.Offices.AnyAsync(p => p.Id == officeId);
        return isExist;
    }
}
