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
        [Required]
        public string PhoneNumber { get; set; }=string.Empty;

        [Required]
        public string Totp { get; set; } = string.Empty;
    }
}
