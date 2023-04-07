using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddSectionValidator : AbstractValidator<AddSectionDTO>
{
    public AddSectionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام بخش ضروری است")
            .Length(1, 50)
            .WithMessage("نام بخش باید بین 1 تا 50 کاراکتر داشته باشد");
    }
}
