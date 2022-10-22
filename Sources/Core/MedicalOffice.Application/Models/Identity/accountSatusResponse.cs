using MedicalOffice.Domain.Enums;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class AccountSatusResponse
    {
        public UserStatus Status { get; set; }

        public bool exist { get; set; } = false;

        public bool OtpOption { get; set; } = false;

        public bool PasswordOption { get; set; } = false;
    }
}