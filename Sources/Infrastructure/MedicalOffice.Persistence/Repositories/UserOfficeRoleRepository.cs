using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class UserOfficeRoleRepository : GenericRepository<UserOfficeRole, Guid>, IUserOfficeRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserOfficeRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles)
    {
        try
        {
            await _dbContext.UserOfficeRoles.AddRangeAsync(userOfficeRoles);
            return true;
        }
        catch (Exception)
        {
            return false;
        }   
    }

    public async Task<List<UserOfficeRole>> GetByUserId(Guid userId)
    {
        var userOfficeRole = await _dbContext.UserOfficeRoles.Where(urf => urf.UserId == userId).ToListAsync();

        return userOfficeRole;
    }

    public async Task<UserOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid userId, Guid? officeId = null)
    {
        var userOfficeRole = new UserOfficeRole
        {
            RoleId = roleId,
            UserId = userId,
        };

        if (officeId != null)
        {
            userOfficeRole.OfficeId = (Guid)officeId;
        }

        await _dbContext.UserOfficeRoles.AddAsync(userOfficeRole);

        return userOfficeRole;
    }
}
