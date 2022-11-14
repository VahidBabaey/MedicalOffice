using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Persistence.Repositories;

public class MedicalStaffRoleRepository : GenericRepository<MedicalStaffRole, Guid>, IMedicalStaffRoleRepository
{
    private readonly IGenericRepository<MedicalStaffRole, Guid> _repositoryMedicalStaffRole;
    private readonly ApplicationDbContext _dbContext;

    public MedicalStaffRoleRepository(IGenericRepository<MedicalStaffRole, Guid> repositoryMedicalStaffRole, ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _repositoryMedicalStaffRole = repositoryMedicalStaffRole;
    }
    public async Task<MedicalStaffRole> InsertToMedicalStaffRole(Guid roleid, Guid medicalstaffid)
    {
        MedicalStaffRole MedicalStaffRole = new()
        {
            RoleId = roleid,
            MedicalStaffId = medicalstaffid
        };

        if (MedicalStaffRole == null)
            throw new NullReferenceException(nameof(MedicalStaffRole));

        await _repositoryMedicalStaffRole.Add(MedicalStaffRole);

        return MedicalStaffRole;
    }

}
