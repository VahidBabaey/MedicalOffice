using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Identity;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class GetByPhoneNumberValidator: AbstractValidator<GetByPhoneNumberDTO>
    {
        public GetByPhoneNumberValidator()
        {
            Include(new PhoneNumberValidator());
        }
    }
}