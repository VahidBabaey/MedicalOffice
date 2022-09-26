using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class PatientCommitmentForm : BaseDomainEntity<Guid>
    { 
        /// <summary>
        /// فرم
        /// </summary>
        public string Form { get; set; } = string.Empty;

        /// <summary>
        /// تاریخ شمسی
        /// </summary>
        public string DateSolar { get; set; } = string.Empty;
        /// <summary>
        /// تاریخ میلادی
        /// </summary>
        public DateTime? DateAD { get; set; }
        /// <summary>
        /// بیمار
        /// </summary>
        public Patient? Patient { get; set; }
        /// <summary>
        /// آی دی بیمار
        /// </summary>
        public Guid? PatientId { get; set; }
    }
}
