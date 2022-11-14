using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
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
    public Guid GetId (Guid id)
    {
        var userOfficeRole =  _dbContext.UserOfficeRoles.Select(p => new {p.Id, p.UserId}).Where(p => p.UserId == id).FirstOrDefault();

        if (userOfficeRole == null)
            throw new NullReferenceException("userOfficeRole Id Not Found!");

        Guid medicalStaffId = userOfficeRole.Id;

        return medicalStaffId;
    }

    public Task<IReadOnlyList<Permission>> GetPermissionDetailsByMedicalStaffID(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SearchMedicalStaff(Guid searchid)
    {
        var idSearchMedicalStaff = await _dbContext.Permissions.Where(p => p.UserOfficeRoleId == searchid).FirstOrDefaultAsync();

        if(idSearchMedicalStaff != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Task<bool> UserHasPermission(Guid userId, Guid officeId, string permission)
    {

        var result = _dbContext.MedicalStaffPermissions.Include(m => m.Permission).Select(x => x.Permission).Any(x => x != null ? x.Name == permission : false);

        return Task.FromResult(result);
    }
}
