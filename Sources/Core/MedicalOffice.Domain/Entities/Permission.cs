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
        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// نام فارسی دسترسی
        /// </summary>
        public string? PersianName { get; set; }

        /// <summary>
        /// شناسه دسته بندی دسترسی
        /// </summary>
        public Guid PermissionCategoryId { get; set; }

        /// <summary>
        ///  ارتباط یک به چند با دسته بندی دسترسی 
        /// </summary>
        public PermissionCategory? PermissionCategory { get; set; }

        /// <summary>
        /// ارتباط چند به چند کاربران مطب و دسترسی هایشان
        /// </summary>
        public ICollection<MedicalStaff> MedicalStaff { get; set; } = new List<MedicalStaff>();

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public Guid? MedicalStaffOfficeRoleId { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public ICollection<MedicalStaffOfficeRole>? MedicalStaffOfficeRole { get; set; }
    }
}
