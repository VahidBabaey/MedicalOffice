using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IUserOfficePermissionRepository : IGenericRepository<UserOfficePermission, Guid>
    {
        Task AddUserOfficePermissions(List<UserOfficePermission> existingUserOfficePermissions, List<UserOfficePermission> userOfficePermissions);
    }
}
