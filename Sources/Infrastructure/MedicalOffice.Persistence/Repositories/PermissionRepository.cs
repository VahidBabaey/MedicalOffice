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
    public async Task<string> GetId (Guid id)
    {
        var userOfficeRole = await _dbContext.UserOfficeRoles.Where(p => p.MedicalStaffId == id).FirstOrDefaultAsync();

        if (userOfficeRole == null)
            throw new NullReferenceException("MedicalStaffOfficeRole Id Not Found!");

        string idUser = userOfficeRole.Id.ToString();

        return idUser;
    }
    public async Task<bool> SearchUser(Guid searchid)
    {
        var idSearchUser = await _dbContext.Permissiones.Where(p => p.MedicalStaffOfficeRoleId == searchid).FirstOrDefaultAsync();

        if(idSearchUser != null)
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
        return (IReadOnlyList<Permission>)await _dbContext.Permissiones.Where(srv => srv.MedicalStaffOfficeRoleId == Id).ToListAsync();
    }

}
