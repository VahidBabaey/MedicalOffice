using FluentValidation;

namespace MedicalOffice.Application.Dtos.SpecializationDTO.Validators;

public class AddSpecializationValidator : AbstractValidator<SpecializationDTO>
{
    public AddSpecializationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100);
    }
}
