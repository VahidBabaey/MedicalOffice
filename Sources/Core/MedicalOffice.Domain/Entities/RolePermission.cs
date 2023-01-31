using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class RolePermission
    {
        /// <summary>
        /// آیدی دسترسی
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// دسترسی
        /// </summary>
        public Permission Permission { get; set; }

        /// <summary>
        /// آیدی نقش
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// نقش
        /// </summary>
        public Role Role { get; set; }
    }
}
