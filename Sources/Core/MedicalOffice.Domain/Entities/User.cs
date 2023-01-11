using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities
{
    public class User :
        IdentityUser<Guid>,
        IPrimaryKeyEntity<Guid>,
        IAuditableEntity,
        ISoftDeletableEntity
    {
        public User()
        {
            SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalID { get; set; } = string.Empty;
        
        /// <summary>
        /// فعال یا غیرفعال
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// ارتباط چند به چند با جدول کاربران مطب ها رول ها
        /// </summary>
        public ICollection<UserOfficeRole>? UserOfficeRoles { get; set; }

        /// <summary>
        /// ارتباط چند به چند با جدول کاربران مطب ها و دسترسی ها
        /// </summary>
        public ICollection<UserOfficePermission>? UserOfficePermissions { get; set; }

        /// <summary>
        /// ایجاد کننده وقت
        /// </summary>
        public ICollection<Appointment> AppointmentsCreatedBy { get; set; }

        /// <summary>
        /// به روزرسانی وقت 
        /// </summary>
        public ICollection<Appointment> AppointmentsLastUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public Guid CreatedById { get; set; }
       
        public DateTime LastUpdatedDate { get; set; }
       
        public Guid LastUpdatedById { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}