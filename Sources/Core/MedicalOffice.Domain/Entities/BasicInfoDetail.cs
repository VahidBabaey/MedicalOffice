using MedicalOffice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Domain.Entities
{
    public class BasicInfoDetail : BaseDomainEntity<Guid>
    {

        /// <summary>
        /// نام جزئیات
        /// </summary>
        public string InfoDetailName { get; set; } = string.Empty;
        /// <summary>
        /// جزئیات
        /// </summary>
        public BasicInfo? basicInfo { get; set; }
        /// <summary>
        /// آیدی جزئیات 
        /// </summary>
        public Guid? basicInfoId { get; set; }

    }
}
