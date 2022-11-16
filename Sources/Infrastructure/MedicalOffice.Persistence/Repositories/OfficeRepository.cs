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
        //List<Office> offices = new List<Office>();
        List<Office?> offices = _dbcontext.UserOfficeRoles.Include(uor => uor.Office).Where(u => u.UserId == userId)
            .Select(uor => uor.Office).ToList();

        //foreach (var item in userOfficeRoles)
        //{
        //    var office = await _dbcontext.Offices.SingleOrDefaultAsync(x => x.Id == item.OfficeId);
        //    if (office != null)
        //    {
        //        offices.Add(office);
        //    }
        //}

        return offices;
    }
}
