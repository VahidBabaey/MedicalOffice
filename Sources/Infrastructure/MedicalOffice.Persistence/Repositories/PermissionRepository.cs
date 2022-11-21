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
    public Guid GetId (Guid id)
    {
        var userOfficeRole =  _dbContext.UserOfficeRoles
            .Select(p => new {p.Id, p.UserId})
            .Where(p => p.UserId == id)
            .FirstOrDefault();

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

    public Task<bool> UserHasPermission(Guid userId, Guid officeId, string[] permission)
    {

        var result = _dbContext.UserOfficePermissions.Include(m => m.Permission)
            .Where(x => x.UserId == userId && x.OfficeId == officeId)
            .Any(x => permission.Contains(x.Permission.Name));

        return Task.FromResult(result);
    }
}
