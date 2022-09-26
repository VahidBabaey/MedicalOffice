using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugD
{
    public class DrugSectionListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// بخش دارویی
        /// </summary>
        public string SectionDrug { get; set; } = string.Empty;
    }
}
