using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class UserOfficePermission : BaseDomainEntity<Guid>
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid OfficeId { get; set; }

        public Office Office { get; set; }

        public Guid PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
