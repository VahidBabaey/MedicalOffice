using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class DrugUsage : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// کاربرد
        /// </summary>
        public string UsageDrug { get; set; } = string.Empty;
        /// <summary>
        /// داروها
        /// </summary>
        public ICollection<Drug>? DrugPres { get; set; }
    }
}
