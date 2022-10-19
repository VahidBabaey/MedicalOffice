﻿using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class MemberShipService : BaseDomainEntity<Guid>
    {
        /// <summary>
        /// عضویت
        /// </summary>
        public Membership? MemberShip { get; set; }
        /// <summary>
        /// آیدی عضویت
        /// </summary>
        public Guid? MembershipId { get; set; }
        /// <summary>
        /// سرویس
        /// </summary>
        public Service? Service { get; set; }
        /// <summary>
        /// آیدی سرویس
        /// </summary>
        public Guid? ServiceId { get; set; }
    }
}
