using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class AccessRepository : GenericRepository<Access, Guid>, IAccessRepository
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

        var idsearchuser = _dbContext.Accesses.Where(p => p.UserOfficeRoleId == searchid).FirstOrDefault();
        if(idsearchuser != null)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public async Task<IReadOnlyList<Access>> GetAccessDetailsByUserID(Guid Id)
    {

        return (IReadOnlyList<Access>)await _dbContext.Accesses.Where(srv => srv.UserOfficeRoleId == Id).ToListAsync();

    }

}
