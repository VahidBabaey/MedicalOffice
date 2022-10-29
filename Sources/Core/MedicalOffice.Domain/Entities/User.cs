using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
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
        /// جنسیت
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string BirthDate { get; set; } = string.Empty;

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalID { get; set; } = string.Empty;

        /// <summary>
        /// وضعیت کاربر
        /// </summary>
        public UserStatus Status{ get; set; }

        /// <summary>
        /// برای ارتباط چند به چند بین کاربران و مطب ها
        /// </summary>
        public ICollection<Office> Office { get; set; } = new List<Office>();

        /// <summary>
        /// برای ایجاد ارتباط چند به چند بین دسترسی ها و کاربران  
        /// </summary>
        public ICollection<Permission> Permissions { get; set; }= new List<Permission>();   
    }
}