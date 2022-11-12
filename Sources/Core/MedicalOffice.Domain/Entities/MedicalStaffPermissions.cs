using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaffPermission
    {
        public Guid MedicalStaffId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
