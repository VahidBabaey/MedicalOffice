using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddSectionValidator : AbstractValidator<AddSectionDTO>
{
    public AddSectionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 50);
    }
}
