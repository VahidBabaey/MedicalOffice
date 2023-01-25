using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class RolePermissionCategory
    {
        public Guid PermissionCategoryId { get; set; }
        public Guid RoleId { get; set; }
        public PermissionCategory PermissionCategory { get; set; }
        public Role Role { get; set; }
    }
}
