using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffOfficeRoleRepository : GenericRepository<MedicalStaffOfficeRole, Guid>, IMedicalStaffOfficeRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffOfficeRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MedicalStaffOfficeRole>> GetByUserId(Guid userId)
    {
        var userOfficeRole = await _dbContext.MedicalStaffOfficeRoles.Where(urf => urf.MedicalStaffId == userId).ToListAsync();

        return userOfficeRole;
    }

    public async Task<MedicalStaffOfficeRole> InsertToUserOfficeRole(Guid roleId, Guid userId, Guid officeId)
    {
        var userOfficeRole = new MedicalStaffOfficeRole
        {
            RoleId = roleId,
            MedicalStaffId = userId,
            OfficeId = officeId
        };

        await _dbContext.MedicalStaffOfficeRoles.AddAsync(userOfficeRole);

        return userOfficeRole;
    }
}
