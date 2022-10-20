using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class AuthenticateByPasswordRequest
    {
        [Required]
        public string? MobilePhone { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}