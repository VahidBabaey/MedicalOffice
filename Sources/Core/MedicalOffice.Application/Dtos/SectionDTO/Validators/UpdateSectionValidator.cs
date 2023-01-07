using FluentValidation;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class UpdateSectionValidator : AbstractValidator<UpdateSectionDTO>
{
    public UpdateSectionValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).Length(1, 50);
    }
}
