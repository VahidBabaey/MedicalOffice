using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    public RegisterUserValidator()
    {
        Include(new IPhoneNumberValidator());
        Include(new INationalIdValidator());

        RuleFor(p=>p.FirstName).NotEmpty().WithMessage("{PropertyName} is required");
    }
}
