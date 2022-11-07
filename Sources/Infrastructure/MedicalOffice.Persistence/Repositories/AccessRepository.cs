using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class AccessRepository : GenericRepository<Permission, Guid>, IPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AccessRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public string GetId (Guid id)
    {

        string iduser = _dbContext.UserOfficeRoles.Where(p => p.MedicalStaffId == id).FirstOrDefault().Id.ToString();
        return iduser;


    }
    public bool SearchUser(Guid searchid)
    {
        var idsearchuser = _dbContext.Permissiones.Where(p => p.MedicalStaffOfficeRoleId == searchid).FirstOrDefault();
        if(idsearchuser != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<IReadOnlyList<Permission>> GetAccessDetailsByUserID(Guid Id)
    {

        return (IReadOnlyList<Permission>)await _dbContext.Permissiones.Where(srv => srv.MedicalStaffOfficeRoleId == Id).ToListAsync();

    }

    public Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id)
    {
        throw new NotImplementedException();
    }

    Task<string> IPermissionRepository.GetId(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<bool> IPermissionRepository.SearchUser(Guid searchid)
    {
        throw new NotImplementedException();
    }
}
