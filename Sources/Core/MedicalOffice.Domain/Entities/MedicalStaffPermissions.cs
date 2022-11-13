using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaffPermission
    {
        public Guid? MedicalStaffId { get; set; }

        public MedicalStaff? MedicalStaff{ get; set; }

        public Guid? PermissionId { get; set; }

        public Permission? Permission { get; set; }
    }
}
