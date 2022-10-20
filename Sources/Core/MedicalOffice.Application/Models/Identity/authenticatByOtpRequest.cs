using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.Identity
{
    public class AuthenticateByOtpRequest
    {
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? OTP { get; set; }
    }
}
