using FluentValidation;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddSectionValidator : AbstractValidator<AddSectionDTO>
{
    private readonly Regex _namePattern;

    public AddSectionValidator()
    {
        _namePattern = new Regex(@"^[آ-ی]{3,}$");
        RuleFor(x => x.Name).NotEmpty().Length(1, 50).Matches(_namePattern);
    }
}
