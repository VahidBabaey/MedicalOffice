using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRoleRepository : GenericRepository<MedicalStaffRole, Guid>, IMedicalStaffRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public Task InsertToMedicalStaffRole(List<MedicalStaffRole> medicalStaffRole)
    {
        _dbContext.MedicalStaffRoles.AddRangeAsync(medicalStaffRole);

        return Task.CompletedTask;
    }

}
