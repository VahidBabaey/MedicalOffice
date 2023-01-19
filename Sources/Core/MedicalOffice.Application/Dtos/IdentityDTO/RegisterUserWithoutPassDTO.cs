using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class RegisterUserWithoutPassDTO : IPhoneNumberDTO, INationalIdDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalID { get; set; } = string.Empty;
    }
}