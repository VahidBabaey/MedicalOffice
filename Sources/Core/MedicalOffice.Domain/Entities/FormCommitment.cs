using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class FormCommitment : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// نام فرم تعهدنامه
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// آیدی بیمار
        /// </summary>
        public Patient? Patient { get; set; }
        /// <summary>
        /// آیدی بیمار
        /// </summary>
        public Guid PatientId { get; set; }


    }
}
