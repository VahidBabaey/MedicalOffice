using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormCommitmentDTO
{
    public class FormCommitmentListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام فرم تعهدنامه
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// نام فرم تعهدنامه
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
