using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO
{
    public class BasicInfoDetailListDTO
    {
        /// <summary>
        /// آیدی اطلاعات پایه
        /// </summary>
        public Guid BasicInfoId { get; set; }

        /// <summary>
        /// لیست جزییات اطلاعات پایه موضوعی
        /// </summary>
        public List<BasicInfoChild> Details { get; set; }

    }
    public class BasicInfoChild : BaseDto<Guid>
    {
        /// <summary>
        /// نام جزئیات
        /// </summary>
        public string InfoDetailName { get; set; } = string.Empty;

    }
}
