using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class authenticateByPasswordRequestDTO
    {
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}