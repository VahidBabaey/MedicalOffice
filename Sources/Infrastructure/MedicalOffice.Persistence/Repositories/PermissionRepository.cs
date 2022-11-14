using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
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
        var UserOfficeRole =  _dbContext.UserOfficeRoles.Select(p => new {p.Id, p.UserId}).Where(p => p.UserId == id).FirstOrDefault().Id;

        if (UserOfficeRole == null)
            throw new NullReferenceException("UserOfficeRole Id Not Found!");

        Guid idMedicalStaff = UserOfficeRole;

        return idMedicalStaff;
    }
    public async Task<bool> SearchMedicalStaff(Guid searchid)
    {
        var idSearchMedicalStaff = await _dbContext.Permissiones.Where(p => p.UserOfficeRoleId == searchid).FirstOrDefaultAsync();

        if(idSearchMedicalStaff != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<IReadOnlyList<Permission>> GetPermissionDetailsByMedicalStaffID(Guid Id)
    {
        return (IReadOnlyList<Permission>)await _dbContext.Permissiones.Where(srv => srv.UserOfficeRoleId == Id).ToListAsync();
    }

}
