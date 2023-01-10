using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class GetByPhoneNumberDTO:IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
