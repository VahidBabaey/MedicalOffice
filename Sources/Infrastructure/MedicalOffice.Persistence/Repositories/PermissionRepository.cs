using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.EntityFrameworkCore;

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
    public async Task<bool> SearchMedicalStaff(Guid searchId)
    {
        var idSearchUser = await _dbContext.Permissiones.Where(p => p.MedicalStaffOfficeRoleId == searchId).FirstOrDefaultAsync();

        if (idSearchUser != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id)
    {
        return await _dbContext.Permissiones.Where(srv => srv.MedicalStaffOfficeRoleId == Id).ToListAsync();
    }

    public Task<List<Permission>> GetByUserAndOfficeId(Guid userId, Guid officeId)
    {
        List<Permission> permissions = _dbContext.MedicalStaffs.Include(m => m.Permission).SingleOrDefault(m => m.UserId == userId && m.OfficeId == officeId).Permission.ToList();
        return Task.FromResult(permissions);
    }
}
