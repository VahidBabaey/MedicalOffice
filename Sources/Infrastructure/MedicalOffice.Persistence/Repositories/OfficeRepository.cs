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
        var offices = await _dbcontext.UserOfficeRoles.Where(u => u.UserId == userId && u.OfficeId != null).Select(u => u.Office).Distinct().ToListAsync();

        return offices;
    }

    public async Task<bool> GetByTelePhoneNumber(string telePhoneNumber)
    {
        var isExist = await _dbcontext.Offices.AnyAsync(x => x.TelePhoneNumber == telePhoneNumber);

        return isExist;
    }

    public async Task<bool> IsOfficeExist(Guid officeId)
    {
        bool isExist = await _dbcontext.Offices.AnyAsync(p => p.Id == officeId);
        return isExist;
    }

    public async Task<bool> isTelePhoneNumberExist(string phone)
    {
        bool isExist = await _dbcontext.Offices.AnyAsync(p => p.TelePhoneNumber == phone);
        return isExist;
    }
}
