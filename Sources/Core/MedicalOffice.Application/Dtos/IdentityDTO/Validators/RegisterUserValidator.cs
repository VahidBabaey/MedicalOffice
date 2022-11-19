using FluentValidation;
using MedicalOffice.Application.CommonValidations;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.Identity.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
{
    private readonly ICommonValidators _validator;
    public RegisterUserValidator(ICommonValidators validator)
    {
        _validator = validator; 
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("The PhoneNumber is required")
            .MaximumLength(11).WithMessage("Maximum length of phone number is 11")
            .Must(x => _validator.ValidPhoneNumber(x).Result).WithMessage("Phone number is not valid");
        RuleFor(x => x.NationalID)
            .NotEmpty().WithMessage("NationalId is required")
            .MaximumLength(10).WithMessage("Maximum length of national id is 10")
            .Must(x => _validator.ValidNationalId(x).Result).WithMessage("National id is not valid");
    }
}
