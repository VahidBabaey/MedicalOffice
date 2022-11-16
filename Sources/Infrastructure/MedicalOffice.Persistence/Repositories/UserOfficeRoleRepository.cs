using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class UserOfficeRoleRepository : GenericRepository<UserOfficeRole, Guid>, IUserOfficeRoleRepository
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
        UserOfficeRole UserOfficeRole = new()
        {
            UserId = userid,
            OfficeId = officeid,
            RoleId = roleid
        };

        if (UserOfficeRole == null)
            throw new NullReferenceException(nameof(UserOfficeRole));

        await _repositoryUserOfficeRole.Add(UserOfficeRole);
        return UserOfficeRole;
    }

    public async Task AddUserOfficeRoles(List<UserOfficeRole> userOfficeRoles)
    {
        await _dbContext.UserOfficeRoles.AddRangeAsync(userOfficeRoles);
        await _dbContext.SaveChangesAsync();
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
