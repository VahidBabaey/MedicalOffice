using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.IdentityDTO;

namespace MedicalOffice.Application.Dtos.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserValidator()
    {
        Include(new PhoneNumberValidator());
        Include(new NationalIdValidator());
    }
}

public class RegisterUserWithoutPassValidator : AbstractValidator<RegisterUserWithoutPassDTO>
{
    public RegisterUserWithoutPassValidator()
    {
        Include(new PhoneNumberValidator());
        Include(new NationalIdValidator());
    }
}