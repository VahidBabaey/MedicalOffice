using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.Identity
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        
        [Required] 
        public string Password { get; set; }
    }
}