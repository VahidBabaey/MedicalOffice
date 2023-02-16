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
        /// تخفیف
        /// </summary>
        public string Discount { get; set; } = string.Empty;
    }
}
