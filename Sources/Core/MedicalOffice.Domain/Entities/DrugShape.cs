using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class DrugShape : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// شکل دارو
        /// </summary>
        public string ShapeDrug { get; set; } = string.Empty;
        /// <summary>
        /// داروها
        /// </summary>
        public ICollection<Drug>? DrugPres { get; set; }
    }
}
