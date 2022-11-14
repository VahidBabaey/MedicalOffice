using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaffRole : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// نقش
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// آیدی نقش
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// کادر درمان
        /// </summary>
        public MedicalStaff MedicalStaff { get; set; }
        /// <summary>
        /// آیدی کادر درمان
        /// </summary>
        public Guid MedicalStaffId { get; set; }
    }
}
