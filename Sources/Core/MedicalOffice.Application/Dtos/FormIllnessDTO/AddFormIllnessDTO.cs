using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormIllnessDTO
{
    public class AddFormIllnessDTO
    {
        /// <summary>
        /// نام فرم استعلاجی
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// نام فرم استعلاجی
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
