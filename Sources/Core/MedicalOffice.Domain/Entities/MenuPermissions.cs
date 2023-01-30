using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MenuPermission
    {
        public Guid MenuId{ get; set; }

        public Menu Menu{ get; set; }

        public Guid PermissionId { get; set; }

        public Permission Permission{ get; set; }
    }
}