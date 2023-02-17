using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security;

namespace MedicalOffice.Persistence.Repositories;

public class PermissionRepository : GenericRepository<Permission, Guid>, IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Permission>> GetByParentIds(List<Guid?> parentIds)
    {
        var permissions = await _dbContext.Permissions.Where(p => parentIds.Contains(p.Id)).ToListAsync();

        return permissions;
    }

    public Task<bool> UserHasPermission(Guid userId, Guid officeId, string[] permission)
    {
        var result = _dbContext.UserOfficePermissions.Include(m => m.Permission)
            .Where(x => x.UserId == userId && x.OfficeId == officeId)
            .Any(x => permission.Contains(x.Permission.Name));

        return Task.FromResult(result);
    }
}
