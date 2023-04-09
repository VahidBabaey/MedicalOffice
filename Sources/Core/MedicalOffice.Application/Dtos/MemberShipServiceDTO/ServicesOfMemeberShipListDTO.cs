using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO
{
    public class ServicesOfMemeberShipListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// سرویس ها
        /// </summary>
        public string ServicesName { get; set; } = string.Empty;

        /// <summary>
        /// آیدی خدمت
        /// </summary>
        public Guid ServiceId { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public string Discount { get; set; } = string.Empty;

        /// <summary>
        /// تاریخ تولید عضویت خدمت
        /// </summary>
        public object CreatedDate { get; set; }
    }
}
