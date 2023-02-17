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
    public class UserOfficePermissionRepository : GenericRelationalEntitiesRepository<UserOfficePermission>, IUserOfficePermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserOfficePermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserOfficePermissions(List<UserOfficePermission> existingUserOfficePermissions, List<UserOfficePermission> userOfficePermissions)
        {

            if (existingUserOfficePermissions.Count != 0)
                _dbContext.UserOfficePermissions.RemoveRange(existingUserOfficePermissions);

            await _dbContext.UserOfficePermissions.AddRangeAsync(userOfficePermissions);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Permission>> GetPermissionsByStaffId(Guid staffId, Guid officeId)
        {
            var userId = _dbContext.MedicalStaffs.Where(x => x.Id == staffId && x.OfficeId == officeId).FirstAsync().Result.UserId;
            
            var permissions = await _dbContext.UserOfficePermissions.Include(c => c.Permission).Where(c => c.OfficeId == officeId && c.UserId == userId).Select(x => x.Permission).ToListAsync();
            
            return permissions;
        }
    }
}
