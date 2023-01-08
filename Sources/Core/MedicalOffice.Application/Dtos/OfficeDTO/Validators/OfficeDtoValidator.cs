using FluentValidation;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators;

public class OfficeValidator : AbstractValidator<OfficeDTO>
{
    public OfficeValidator()
    {
        Include(new TelePhoneNumberValidator());

        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(3)
            .WithMessage("minimum length of {PropertyName} is 3");

        RuleFor(o => o.Address)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MinimumLength(10)
            .WithMessage("minimum length of {PropertyName} is 10");
    }
}
