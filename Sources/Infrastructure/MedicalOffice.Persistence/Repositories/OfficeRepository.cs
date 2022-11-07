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
        //List<OfficeFeature> offices = await _dbcontext.Offices.Include(s => s.MedicalStaffOfficeRoles.Where(x => x.MedicalStaffId == userId)).ToListAsync();
        //List<Office> offices = await _dbcontext.Offices.Where(o => o.User == new User
        //{
        //    Id = userId,
        //}).ToListAsync();

        //List<Office> offices = await _dbcontext.Offices.Include(o => o.User.Select(o => new
        //{
        //    Id = userId,
        //})).ToListAsync();  

        List<Office> offices = await _dbcontext.Offices.Include(o=>o.User).Where(o=>o.UserId == userId).ToListAsync();  

        return offices;
    }
}
