using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO
{
    public class BasicInfoDetailDTO : IBasicInfoDetailIdDTO
    {
        /// <summary>
        /// نام جزئیات
        /// </summary>
        public string InfoDetailName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی اطلاعات پایه
        /// </summary>
        public Guid BasicInfoId { get; set; }
    }
}
