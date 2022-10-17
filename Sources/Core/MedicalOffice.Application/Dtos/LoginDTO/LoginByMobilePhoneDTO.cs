using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.LoginDTO
{
    public class LoginByMobilePhoneDTO
    {
        [Required]
        public string? MobilePhone { get; set; }

        [Required]
        public string? OTP { get; set; }
    }
}
