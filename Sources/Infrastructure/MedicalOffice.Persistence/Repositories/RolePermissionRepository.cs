using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class RolePermissionRepository : GenericRelationalEntitiesRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RolePermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Permission>> GetByRoleId(Guid roleId)
        {
            var permissions = await _dbContext.RolePermissions.Where(c => c.RoleId == roleId).Select(c => c.Permission).ToListAsync();

            return permissions;
        }
    }
}
