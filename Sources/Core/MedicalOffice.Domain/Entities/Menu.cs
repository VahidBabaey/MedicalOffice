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
        /// <summary>
        /// شناسه Parent
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام فارسی
        /// </summary>
        public string PersianName { get; set; }

        /// <summary>
        /// لینک
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// فعال یا غیرفعال
        /// </summary>
        public bool IsActive { get; set; } = true;

        public ICollection<MenuPermission> MenuPermissions { get; set; }
    }
}
