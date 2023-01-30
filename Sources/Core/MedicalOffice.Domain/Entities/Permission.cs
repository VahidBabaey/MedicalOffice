using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class Permission : BaseDomainEntity<Guid>
    {
        public Guid? ParentId { get; set; }

        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام فارسی دسترسی
        /// </summary>
        public string PersianName { get; set; }

        /// <summary>
        /// نمایش در دسترسی ها
        /// </summary>
        public bool IsShown { get; set; } = true;

        /// <summary>
        /// ارتباط چند به چند کاربران مطب و دسترسی هایشان
        /// </summary>
        public ICollection<UserOfficePermission> UserOfficePermissions { get; set; } = new List<UserOfficePermission>();
    }
}
