using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Picture : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// مسیر عکس
        /// </summary>
        public string VirtualPath { get; set; } = string.Empty;
        /// <summary>
        /// نام عکس
        /// </summary>
        public string PictureName { get; set; } = string.Empty;
        /// <summary>
        /// آفیس
        /// </summary>
        public Office? Office { get; set; }
        /// <summary>
        /// آیدی آفیس
        /// </summary>
        public Guid OfficeId { get; set; }
        /// <summary>
        /// بیمار
        /// </summary>
        public Patient? Patient { get; set; }
        /// <summary>
        /// آیدی بیمار
        /// </summary>
        public Guid PatientId { get; set; }
    }
}
