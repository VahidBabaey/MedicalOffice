using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ShiftDTO
{
    public class ShiftDTO
    {
        /// <summary>
        /// عنوان شیفت : صبح - ظهر - عصر - غیره
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// تایم شروع
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// تایم پایان
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// آیا شیفت روز بعد هست؟
        /// </summary>
        public bool NextDay { get; set; } = false;
    }
}
