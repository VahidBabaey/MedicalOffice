using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class VerifyTotpDTO : IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string Totp { get; set; }
    }
}