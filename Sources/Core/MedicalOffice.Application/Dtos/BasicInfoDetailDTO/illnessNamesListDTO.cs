using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO
{
    public class illnessNamesListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام جزئیات
        /// </summary>
        public string illnessName { get; set; } = string.Empty;
    }
}
