using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class RegisterUserWithoutPassDTO : IPhoneNumberDTO, INationalIdDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;
    }
}