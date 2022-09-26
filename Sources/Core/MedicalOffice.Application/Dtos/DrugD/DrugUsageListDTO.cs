using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugD
{
    public class DrugUsageListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// کاربرد
        /// </summary>
        public string UsageDrug { get; set; } = string.Empty;
    }
}
