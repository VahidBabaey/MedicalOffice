using FluentValidation;
using MedicalOffice.Application.Dtos.ExperimentDTO;

namespace MedicalOffice.Application.Dtos.ExperimentDTO.Validators;

public class AddExperimentValidator : AbstractValidator<ExperimentDTO>
{
    public AddExperimentValidator()
    {
        RuleFor(x => x.GenericCode).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.AnswerType).NotEmpty();
    }
}
