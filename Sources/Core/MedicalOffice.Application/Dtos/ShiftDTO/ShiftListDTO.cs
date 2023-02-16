using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ShiftDTO
{
     public class ShiftListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// عنوان شیفت : صبح - ظهر - عصر - غیره
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// تایم شروع
        /// </summary>
        public string StartTime { get; set; } = string.Empty;
        /// <summary>
        /// تایم پایان
        /// </summary>
        public string EndTime { get; set; } = string.Empty;
        /// <summary>
        /// آیا شیفت روز های تعطبل نیز هست یا خیر
        /// </summary>
        public bool HolidayShift { get; set; }
    }
}
