using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Introducer : BaseDomainEntity<Guid>
    {    /// <summary>
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
        public string Firstname { get; set; } = string.Empty;
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Lastname { get; set; } = string.Empty;
        /// <summary>
        /// نوع معرف
        /// </summary>
        public IntroducerType? IntroducerType { get; set; }
        /// <summary>
        /// بیمار
        /// </summary>
        public Patient? Patient { get; set; }
        /// <summary>
        /// آیدی بیمار
        /// </summary>
        public Guid? PatientId { get; set; }
        /// <summary>
        /// کادر درمان
        /// </summary>
        public MedicalStaff? MedicalStaff { get; set; }
        /// <summary>
        /// آیدی مادر درمان
        /// </summary>
        public Guid? MedicalStaffId { get; set; }
    }
}
