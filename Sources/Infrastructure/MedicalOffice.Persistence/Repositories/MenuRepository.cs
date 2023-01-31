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
    public class MenuRepository : GenericRepository<Menu, Guid>, IMenuRepository
    {
        ApplicationDbContext _dbContext;
        public MenuRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Menu>> GetAllByUserId(Guid userId, Guid officId, List<Guid> roleIds)
        {
            var permissions = await _dbContext.UserOfficePermissions.Where(x => x.UserId == userId && x.OfficeId == officId).Select(x => x.PermissionId).ToListAsync();
            if (permissions == null)
            {
                permissions = await _dbContext.RolePermissions.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.PermissionId).ToListAsync();
            }
            var menu = await _dbContext.MenuPermissions.Where(x => permissions.Contains(x.PermissionId)).Select(x => x.Menu).ToListAsync();

            return menu;
        }
    }
}
