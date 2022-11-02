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
        /// کد ملی
        /// </summary>
        public string NationalId { get; set; } = string.Empty;

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