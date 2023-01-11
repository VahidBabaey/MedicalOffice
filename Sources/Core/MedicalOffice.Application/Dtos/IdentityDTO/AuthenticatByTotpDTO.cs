using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class AuthenticateByTotpDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string Totp { get; set; } = string.Empty;
    }
}
