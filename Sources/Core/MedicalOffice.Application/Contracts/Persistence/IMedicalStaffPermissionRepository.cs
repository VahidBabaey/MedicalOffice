using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMedicalStaffPermissionRepository : IGenericRepository<MedicalStaffPermission, Guid>
    {
    }
}
