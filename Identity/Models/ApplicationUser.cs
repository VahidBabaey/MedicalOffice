using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
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
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// هش رمز ورود
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// پذیرش ها
        /// </summary>
        public ICollection<Reception>? Receptions { get; set; }
        /// <summary>
        /// کاربران پذیرش
        /// </summary>
        public ICollection<ReceptionUser>? ReceptionUsers { get; set; }
        /// <summary>
        /// وقت دهی ها
        /// </summary>
        public ICollection<Appointment>? Appointments { get; set; }
    }

}