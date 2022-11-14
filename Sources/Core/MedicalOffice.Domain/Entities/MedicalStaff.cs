using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MedicalStaff : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// مطب
        /// </summary>
        public Office? Office { get; set; }
        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid? OfficeId { get; set; }
        /// <summary>
        /// کاربر
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// عکس کادر درمان
        /// </summary>
        public byte[]? ProfilePicture { get; set; }
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
        public DoctorTopic? DoctorTopic { get; set; }
        /// <summary>
        /// تخصص
        /// </summary>
        public Specialization? Specialization { get; set; }
        /// <summary>
        /// آیدی تخصص
        /// </summary>
        public Guid? SpecializationId { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// پسورد
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        /// <summary>
        /// برنامه کادر درمان
        /// </summary>
        public ICollection<MedicalStaffWorkHourProgram> MedicalStaffWorkHourPrograms { get; set; } = new List<MedicalStaffWorkHourProgram>();
        /// <summary>
        /// پذیرش ها
        /// </summary>
        public ICollection<Reception>? Receptions { get; set; }
        /// <summary>
        /// کاربران پذیرش
        /// </summary>
        public ICollection<ReceptionMedicalStaff>? ReceptionMedicalStaffs { get; set; }
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
