﻿using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.OfficeDTO
{
    public class OfficeDTO: ITelePhoneNumberDTO
    {
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// شماره ثابت
        /// </summary>
        public string TelePhoneNumber { get; set; } = string.Empty;
    }
}
