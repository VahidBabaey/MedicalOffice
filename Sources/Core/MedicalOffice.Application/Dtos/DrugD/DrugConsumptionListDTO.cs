using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugD
{
    public class DrugConsumptionListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// شکل دارو
        /// </summary>
        public string ConsumptionDrug { get; set; } = string.Empty;
    }
}
