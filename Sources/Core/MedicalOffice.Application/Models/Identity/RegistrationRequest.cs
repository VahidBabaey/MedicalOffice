using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
