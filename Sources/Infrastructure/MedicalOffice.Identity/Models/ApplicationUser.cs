using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
     
        public string LastName { get; set; } = string.Empty;

        public Gender? Gender { get; set; }
     
        public string BirthDate { get; set; } = string.Empty;
  
        public string NationalID { get; set; } = string.Empty;
 
        public string Username { get; set; } = string.Empty;
       
        public string PasswordHash { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        
        public Role Role { get; set; }
    }
}