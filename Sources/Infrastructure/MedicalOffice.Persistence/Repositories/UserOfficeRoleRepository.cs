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
}
