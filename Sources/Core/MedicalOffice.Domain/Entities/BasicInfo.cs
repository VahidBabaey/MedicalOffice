using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class BasicInfo : BaseDomainEntity<Guid>
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
        /// نام
        /// </summary>
        public string InfoName { get; set; } = string.Empty;
        /// <summary>
        /// لیست جزِئیات
        /// </summary>
        public ICollection<BasicInfoDetail>? BasicInfoDetails { get; set; } = new List<BasicInfoDetail>();

    }
}
