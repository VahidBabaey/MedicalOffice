using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoListDTO
{
    public class BasicInfoListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام
        /// </summary>
        public string InfoName { get; set; } = string.Empty;
    }
}
