using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaff : BaseDomainEntity<Guid>
    {

        /// <summary>
        /// عکس کادر درمان
        /// </summary>
        //public byte[]? ProfilePicture { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// شماره نظام پزشکی
        /// </summary>
        public string MedicalNumber { get; set; } = string.Empty;

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalID { get; set; } = string.Empty;

        /// <summary>
        /// عنوان
        /// </summary>
        public Title? Title { get; set; }

        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// شناسه کاربر سیستم
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// (ارتباط یک به چند کاربر مطب با کاربر سیستم (هر کاربر سیستم میتواند در هر مطب کاربر متفاوتی باشد
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid? OfficeId { get; set; }

        /// <summary>
        /// ارتباط یک به چند کاربر مطب با مطب
        /// </summary>
        public Office? Office { get; set; }

        /// <summary>
        /// شناسه نقش کاربر مطب 
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// برای ارتباط یک به چند یک نقش با کاربران مطب
        /// </summary>
        public Role? Role { get; set; }

        /// <summary>
        /// آیدی تخصص
        /// </summary>
        public Guid? SpecializationId { get; set; }

        /// <summary>
        /// تخصص
        /// </summary>
        public Specialization? Specialization { get; set; }

        /// <summary>
        /// برنامه کادر درمان
        /// </summary>
        public ICollection<MedicalStaffSchedule> MedicalStaffSchedules { get; set; } = new List<MedicalStaffSchedule>();
        
        /// <summary>
        /// پذیرش ها
        /// </summary>
        public ICollection<Reception>? Receptions { get; set; }    
        /// <summary>
        /// وقت دهی ها
        /// </summary>
        public ICollection<Appointment>? Appointments { get; set; }
        
        /// <summary>
        /// کادر درمان - نقش
        /// </summary>
        public ICollection<MedicalStaffRole>? MedicalStaffRoles { get; set; }
    }
}
