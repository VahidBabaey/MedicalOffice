using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class User : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// مطب
        /// </summary>
        public Office? Office { get; set; }
        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid OfficeId { get; set; }
        /// <summary>
        /// کادر درمان
        /// </summary>
        public ICollection<MedicalStaff>? MedicalStaffs { get; set; }
        /// <summary>
        /// کاربر - آفیس - نقش
        /// </summary>
        public ICollection<UserOfficeRole>? UserOfficeRoles { get; set; }
    }
}
