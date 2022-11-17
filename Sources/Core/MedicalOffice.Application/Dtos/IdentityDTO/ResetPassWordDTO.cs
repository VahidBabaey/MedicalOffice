namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class ResetPassWordDTO
    {
        public string PhoneNumber { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}