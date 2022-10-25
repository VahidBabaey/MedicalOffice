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

            Offices=new List<Office>(); 
            Permissions=new List<Permission>();
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
        /// از این مدل برای برقراری ارتباط یک به چند بین کاربر و کاربر-مطب-نقش استفاده می شود
        /// </summary>

        public ICollection<Office> Offices { get; set; }

        //public Guid? PermissionId { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}