using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.IdentityDTO
{
    public class ResetPasswordDTO:IPhoneNumberDTO
    {
        public string PhoneNumber { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}