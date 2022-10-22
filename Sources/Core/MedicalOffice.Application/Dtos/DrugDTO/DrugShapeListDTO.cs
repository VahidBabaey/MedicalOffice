using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugDTO
{
    public class DrugShapeListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// شکل دارو
        /// </summary>
        public string ShapeDrug { get; set; } = string.Empty;
    }
}
