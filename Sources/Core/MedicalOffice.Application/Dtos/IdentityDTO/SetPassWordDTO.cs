using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class SetPasswordDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}