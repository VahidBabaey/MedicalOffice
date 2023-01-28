using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class RolePermission
    {
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public Permission Permission{ get; set; }
        public Role Role { get; set; }
    }
}
