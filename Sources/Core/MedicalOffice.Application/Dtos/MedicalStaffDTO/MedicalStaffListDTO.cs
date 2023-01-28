using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO
{
    public class MedicalStaffListDTO : BaseDto<Guid>
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
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// شماره نظام پزشکی
        /// </summary>
        public string MedicalNumber { get; set; } = string.Empty;
        /// <summary>
        /// آیدی تخصص
        /// </summary>
        public Guid? SpecializationId { get; set; }
        /// <summary>
        /// نام تخصص
        /// </summary>
        public string SpecializationName { get; set; } = string.Empty;

    }
}
