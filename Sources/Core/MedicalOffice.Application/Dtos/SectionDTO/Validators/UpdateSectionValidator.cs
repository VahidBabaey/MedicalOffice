using FluentValidation;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class UpdateSectionValidator : AbstractValidator<UpdateSectionDTO>
{
    public UpdateSectionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام بخش ضروری است")
            .Length(1, 50)
            .WithMessage("نام بخش باید بین 1 تا 50 کاراکتر داشته باشد");
    }
}
