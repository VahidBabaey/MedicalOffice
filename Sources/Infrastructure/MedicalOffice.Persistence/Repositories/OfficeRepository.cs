using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace MedicalOffice.Persistence.Repositories;

public class OfficeRepository : GenericRepository<Office, Guid>, IOfficeRepository
{
    private readonly ApplicationDbContext _dbcontext;
    public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<List<Office>> GetByUserId(Guid userId)
    {
        var offices = await _dbcontext.UserOfficeRoles.Where(u => u.UserId == userId && u.OfficeId != null).Select(uor => uor.Office).ToListAsync();

        return offices;
    }
    public async Task<bool> CheckExistOfficeId(Guid officeId)
    {
        bool isExist = await _dbcontext.Offices.AnyAsync(p => p.Id == officeId);
        return isExist;
    }

    public async Task<bool> GetByTelePhoneNumber(string telePhoneNumber)
    {
        var isExist = await _dbcontext.Offices.AnyAsync(x => x.TelePhoneNumber == telePhoneNumber);

        return isExist;
    }
}
