using MedicalOffice.Application.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class AuthenticateByPasswordDTO
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}