using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.ApiConsumerModels
{
    public class PatientInquireDTO
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
        /// کد ملی
        /// </summary>
        public string? NationalId { get; set; } = string.Empty;

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string? BirthDate { get; set; } = string.Empty;

        /// <summary>
        /// نام پدر
        /// </summary>
        public string? FatherName { get; set; } = string.Empty;

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// بیمه
        /// </summary>
        public string InsuranceName { get; set; }

        /// <summary>
        /// سن
        /// </summary>
        public int? Age { get; set; }
    }
}
