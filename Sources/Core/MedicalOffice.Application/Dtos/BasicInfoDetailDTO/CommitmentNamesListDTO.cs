using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO
{
    public class CommitmentNamesListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام تعهد
        /// </summary>
        public string CommitmentName { get; set; } = string.Empty;
    }
}
