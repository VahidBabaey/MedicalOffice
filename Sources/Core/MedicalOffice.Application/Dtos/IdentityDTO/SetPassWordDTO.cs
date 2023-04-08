using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class SetPasswordDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}