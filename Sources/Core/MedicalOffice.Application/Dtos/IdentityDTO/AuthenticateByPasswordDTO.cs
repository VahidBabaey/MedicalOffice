using MedicalOffice.Application.Dtos.Common.IDtos;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class AuthenticateByPasswordDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}