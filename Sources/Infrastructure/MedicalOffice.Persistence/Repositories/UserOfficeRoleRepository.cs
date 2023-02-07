using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class UserOfficeRoleRepository : GenericJointEntitiesRepository<UserOfficeRole>, IUserOfficeRoleRepository
{
    private readonly IGenericRepository<UserOfficeRole, Guid> _repositoryUserOfficeRole;

    private readonly ApplicationDbContext _dbContext;

    public UserOfficeRoleRepository(IGenericRepository<UserOfficeRole, Guid> repositoryUserOfficeRole, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _repositoryUserOfficeRole = repositoryUserOfficeRole;
    }
    public async Task<UserOfficeRole> InsertToUserOfficeRole(Guid userid, Guid roleid, Guid officeid)
    {
        var result = await _repositoryUserOfficeRole.Add(new()
        {
            UserId = userid,
            OfficeId = officeid,
            RoleId = roleid
        });

        return result;
    }

    public async Task AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles)
    {
        await _dbContext.UserOfficeRoles.AddRangeAsync(userOfficeRoles);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserOfficeRole>> GetByUserId(Guid userId)
    {
        var userOfficeRole = await _dbContext.UserOfficeRoles.Include(x => x.Role).Where(urf => urf.UserId == userId).ToListAsync();

        return userOfficeRole;
    }

    public async Task<List<UserOfficeRole>> GetByUserAndOfficeId(Guid userId, Guid officeId)
    {
        var userOfficeRole = await _dbContext.UserOfficeRoles.Where(urf => urf.UserId == userId && urf.OfficeId == officeId).ToListAsync();

        return userOfficeRole;
    }

    public async Task DeleteUserOfficeRoleAsync(Guid userId, Guid OfficeId)
    {
        var medicalStaffs = await _dbContext.UserOfficeRoles.Where(ur => ur.UserId == userId && ur.OfficeId == OfficeId).ToListAsync();

        _dbContext.UserOfficeRoles.RemoveRange(medicalStaffs);

        _dbContext.SaveChanges();
    }
}
