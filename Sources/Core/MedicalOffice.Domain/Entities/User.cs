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
            Office = new List<Office>();
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
        /// شناسه مطب
        /// </summary>
        public Guid OfficeId{ get; set; }

        /// <summary>
        /// برای ارتباط چند به چند بین کاربران و مطب ها
        /// </summary>
        public ICollection<Office> Office { get; set; }

        /// <summary>
        /// برای ایجاد ارتباط چند به چند بین دسترسی ها و کاربران  
        /// </summary>
        public ICollection<Permission> Permission { get; set; }= new List<Permission>();   
    }
}