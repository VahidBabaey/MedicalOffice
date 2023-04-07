
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO
{
    public class UpdateMemberShipServiceDTO : BaseDto<Guid> , IMembershipIdDTO, IServiceIdDTO
    {
        /// <summary>
        /// آیدی عضویت
        /// </summary>
        public Guid MembershipId { get; set; }

        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// تخفیف
        /// </summary>
        public int Discount { get; set; }
    }
}
