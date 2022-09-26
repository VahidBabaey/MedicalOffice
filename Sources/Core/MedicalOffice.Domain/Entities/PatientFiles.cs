using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class PatientFiles : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// فایل بیمار
        /// </summary>
        public byte[]? File { get; set; }
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
