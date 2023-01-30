using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Menu : BaseDomainEntity<Guid>
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string PersianName { get; set; }
        public string? Link { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
