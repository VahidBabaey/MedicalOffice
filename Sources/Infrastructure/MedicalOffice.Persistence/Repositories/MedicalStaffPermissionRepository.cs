using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.Repositories
{
    public class MedicalStaffPermissionRepository : GenericRepository<MedicalStaffPermission, Guid>, IMedicalStaffPermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicalStaffPermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
