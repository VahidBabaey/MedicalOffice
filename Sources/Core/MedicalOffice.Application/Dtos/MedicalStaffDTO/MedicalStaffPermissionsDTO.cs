using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO
{
    public class MedicalStaffPermissionsDTO
    {
        public Guid MedicalStaffId { get; set; }

        public Guid[] PermissionIds { get; set; } = new Guid[0];
    }
}
