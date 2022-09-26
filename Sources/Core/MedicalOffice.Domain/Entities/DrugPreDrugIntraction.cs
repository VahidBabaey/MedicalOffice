using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class DrugPreDrugIntraction : BaseDomainEntity2
    {
        public Guid DrugPreId { get; set; }
        public Guid DrugIntractionId { get; set; }
        public virtual Drug DrugPre { get; set; }
        public virtual DrugIntraction DrugIntraction { get; set; }
    }
}
