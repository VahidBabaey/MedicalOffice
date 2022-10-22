using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Identity.Model
{
    public class User: IdentityUser<Guid> // BaseDomainEntity<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        //public string PhoneNumber { get; set; } = String.Empty;
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
        /// نام کاربری
        /// </summary>
        //public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// هش رمز ورود
        /// </summary>
        //public string PasswordHash { get; set; } = string.Empty;
        ///// <summary>
        ///// از این مدل برای برقراری ارتباط یک به چند بین کاربر و کاربر-مطب-نقش استفاده می شود
        ///// </summary>
        //public ICollection<UserOfficeRole>? UserOfficeRoles { get; set; }
    }
}