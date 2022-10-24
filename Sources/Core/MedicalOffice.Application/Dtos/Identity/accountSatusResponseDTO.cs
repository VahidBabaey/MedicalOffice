using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.Identity
{
    public class accountSatusResponseDTO
    {
        public UserStatus Status { get; set; }

        public bool Exist { get; set; } = false;

        public bool OtpOption { get; set; } = false;

        public bool PasswordOption { get; set; } = false;
    }
}