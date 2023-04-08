using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormReferalDTO
{
    public class UpdateFormReferalDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام فرم ارجاع
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// نام فرم ارجاع
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
