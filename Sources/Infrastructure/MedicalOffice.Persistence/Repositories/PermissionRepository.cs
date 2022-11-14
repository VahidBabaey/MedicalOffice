using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MedicalOffice.Persistence.Repositories;

public class PermissionRepository : GenericRepository<Permission, Guid>, IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> GetId(Guid id)
    {
        var userOfficeRole = await _dbContext.UserOfficeRoles.Where(p => p.UserId == id).FirstOrDefaultAsync();

        if (userOfficeRole == null)
            throw new NullReferenceException("MedicalStaff Id Not Found!");

        string idUser = userOfficeRole.Id.ToString();

        return idUser;
    }
    //public async Task<bool> SearchMedicalStaff(Guid searchId)
    //{
    //    var idSearchUser = await _dbContext.Permissiones.Where(p => p.UserOfficeRoleId == searchId).FirstOrDefaultAsync();

    //    if (idSearchUser != null)
    //        return true;

    //    else
    //        return false;

    //}
    public async Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id)
    {
        return await _dbContext.Permissions.Where(srv => srv.UserOfficeRoleId == Id).ToListAsync();
    }

    public Task<bool> UserHasPermission(Guid userId, Guid officeId, string permission)
    {

        var result = _dbContext.MedicalStaffPermissions.Include(m => m.Permission).Select(x => x.Permission).Any(x => x != null ? x.Name == permission : false);

        return Task.FromResult(result);
    }
}
