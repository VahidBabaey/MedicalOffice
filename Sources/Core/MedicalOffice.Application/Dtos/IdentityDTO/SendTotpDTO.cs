using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class SendTotpDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }
    }
}
