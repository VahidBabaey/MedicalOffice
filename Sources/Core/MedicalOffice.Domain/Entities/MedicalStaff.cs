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
        public Guid UserId { get; set; }

        public User? User { get; set; }

        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// مطب
        /// </summary>
        public Office? Office { get; set; }

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
        /// آیدی تخصص
        /// </summary>
        public Guid SpecializationId { get; set; } = new Guid();

        /// <summary>
        /// تخصص
        /// </summary>
        public Specialization? Specialization { get; set; }

        /// <summary>
        /// برنامه کادر درمان
        /// </summary>   
        public ICollection<MedicalStaffWorkHourProgram> UserWorkHourPrograms { get; set; } = new List<MedicalStaffWorkHourProgram>();
        
        /// <summary>
        /// از این مدل برای برقراری ارتباط یک به چند بین نقش و کاربر-مطب-نقش استفاده می شود
        /// </summary>
        public ICollection<MedicalStaffOfficeRole>? UserOfficeRoles { get; set; }
        
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