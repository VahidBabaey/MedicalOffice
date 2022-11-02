using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string NationalID { get; set; } = string.Empty;

    }
}
