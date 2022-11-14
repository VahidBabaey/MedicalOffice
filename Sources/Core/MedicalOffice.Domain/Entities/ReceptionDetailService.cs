using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class ReceptionDetailService : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// جزئیات پذیرش
        /// </summary>
        public ReceptionDetail? ReceptionDetail { get; set; }
        /// <summary>
        /// آیدی جزئیات پذیرش
        /// </summary>
        public Guid? ReceptionDetailId { get; set; }
        /// <summary>
        /// سرویس
        /// </summary>
        public Service? Service { get; set; }
        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public Guid? ServiceId { get; set; }
    }
}
