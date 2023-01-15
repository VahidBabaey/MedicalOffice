using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO
{
    public class MemberShipServiceDTO : IMembershipIdDTO, IServiceListIdDTO
    {
        /// <summary>
        /// آیدی عضویت
        /// </summary>
        public Guid MembershipId { get; set; }
        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public Guid[] ServiceId { get; set; }
        /// <summary>
        /// تخفیف
        /// </summary>
        public string Discount { get; set; } = string.Empty;
    }
}
