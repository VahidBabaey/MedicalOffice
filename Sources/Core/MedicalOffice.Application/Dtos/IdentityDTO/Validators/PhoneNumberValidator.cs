using FluentValidation;
using MedicalOffice.Application.Dtos.Identity;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class PhoneNumberValidator: AbstractValidator<PhoneNumberDTO>
    {
        public PhoneNumberValidator()
        {

        }
    }
}