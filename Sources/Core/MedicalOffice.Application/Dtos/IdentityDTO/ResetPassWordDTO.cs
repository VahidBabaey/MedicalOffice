using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class ResetPasswordDTO:IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}