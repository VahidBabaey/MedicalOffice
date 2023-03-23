using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BankDTO
{
    public class BankListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// بانک
        /// </summary>
        public string BankName { get; set; } = string.Empty;
    }
}
