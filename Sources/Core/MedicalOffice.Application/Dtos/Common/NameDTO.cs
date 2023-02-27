using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common
{
    public class NameDTO : BaseDto<Guid>
    {
        public string Name{ get; set; }
    }
}