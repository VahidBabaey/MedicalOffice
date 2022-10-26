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
    public Guid GetId (Guid id)
    {
        var userOfficeRole =  _dbContext.UserOfficeRoles.Select(p => new {p.Id, p.UserId}).Where(p => p.UserId == id).FirstOrDefault().Id;

        if (userOfficeRole == null)
            throw new NullReferenceException("UserOfficeRole Id Not Found!");

        Guid idUser = userOfficeRole;

        return idUser;
    }
    public async Task<bool> SearchUser(Guid searchid)
    {
        var idSearchUser = await _dbContext.Permissiones.Where(p => p.UserOfficeRoleId == searchid).FirstOrDefaultAsync();

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
        return (IReadOnlyList<Permission>)await _dbContext.Permissiones.Where(srv => srv.UserOfficeRoleId == Id).ToListAsync();
    }

}
