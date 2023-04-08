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
        /// نام
        /// </summary>
        public string InfoName { get; set; }

        /// <summary>
        /// ترتیب
        /// </summary>
        public sbyte Order { get; set; }

        /// <summary>
        /// ترتیب
        /// </summary>
        public bool isActive { get; set; }

        /// <summary>
        /// لیست جزِئیات
        /// </summary>
        public ICollection<BasicInfoDetail>? BasicInfoDetails { get; set; } = new List<BasicInfoDetail>();
    }
}
