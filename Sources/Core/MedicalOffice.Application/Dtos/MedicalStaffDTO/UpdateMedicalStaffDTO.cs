﻿using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO
{
    public class UpdateMedicalStaffDTO : BaseDto<Guid>, IPhoneNumberDTO, INationalIdDTO
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
        public string NationalId { get; set; } = string.Empty;

        /// <summary>
        /// عنوان
        /// </summary>
        public Title? Title { get; set; }

        /// <summary>
        /// آیدی تخصص
        /// </summary>
        public Guid? SpecializationId { get; set; }

        /// <summary>
        /// نام کاربری بیمه سلامت
        /// </summary>
        public string IHIOUserName { get; set; } = string.Empty;

        /// <summary>
        /// رمز عبور بیمه سلامت
        /// </summary>
        public string IHIOPassword { get; set; } = string.Empty;

        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// پسورد
        /// </summary>
        //public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// لیست نقش ها
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// مسئول  فنی
        /// </summary>
        public bool IsTechnicalAssistant { get; set; }
        /// <summary>
        /// معرف
        /// </summary>
        public bool IsReferrer { get; set; }
        /// <summary>
        /// متخصص
        /// </summary>
        public bool IsSpecialist { get; set; }
    }
}
