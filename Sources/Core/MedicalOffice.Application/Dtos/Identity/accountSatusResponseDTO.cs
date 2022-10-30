using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class accountSatusResponseDTO
    {
        public UserActivationStatus ActivationStatus { get; set; } = UserActivationStatus.active;

        public bool Exist { get; set; } = true;

        public bool OtpOption { get; set; } = true;

        public bool PasswordOption { get; set; } = true;
    }
}