using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PatientReferralFormDTO
{
    public class PatientReferralFormListDTO
    {
        /// <summary>
        /// علت
        /// </summary>
        public string ReferralReason { get; set; } = string.Empty;
        /// <summary>
        /// تاریخ شمسی
        /// </summary>
        public string DateSolar { get; set; } = string.Empty;
        /// <summary>
        /// تاریخ میلادی
        /// </summary>
        public DateTime? DateAD { get; set; }
        /// <summary>
        /// آی دی بیمار
        /// </summary>
        public Guid? PatientId { get; set; }
    }
}
