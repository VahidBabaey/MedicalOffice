using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class UserOfficePermissionRepository : GenericRepository<UserOfficePermission, Guid>, IUserOfficePermissionRepository
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
    }
}
