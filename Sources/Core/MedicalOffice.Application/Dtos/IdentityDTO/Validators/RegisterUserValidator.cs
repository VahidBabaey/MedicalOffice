using FluentValidation;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.IdentityDTO;

namespace MedicalOffice.Application.Dtos.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserValidator()
    {
        Include(new IPhoneNumberValidator());
        Include(new INationalIdValidator());
    }
}

public class RegisterUserWithoutPassValidator : AbstractValidator<RegisterUserWithoutPassDTO>
{
    public RegisterUserWithoutPassValidator()
    {
        Include(new IPhoneNumberValidator());
        Include(new INationalIdValidator());
    }
}