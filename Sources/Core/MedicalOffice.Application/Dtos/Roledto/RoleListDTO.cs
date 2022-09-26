using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Roledto
{
    public class RoleListDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
