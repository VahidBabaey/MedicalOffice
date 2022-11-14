using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO
{
    public class UpdateMedicalStaffDTO : BaseDto<Guid>
    {
        /// <summary>
        /// آیدی مطب
        /// </summary>
        public Guid? OfficeId { get; set; }
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
        /// لیست نقش ها
        /// </summary>
        public Guid[]? RoleIds { get; set; }
    }
}
