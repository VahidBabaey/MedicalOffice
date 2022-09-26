using FluentValidation;
using MedicalOffice.Application.Dtos.Experiment;

namespace MedicalOffice.Application.Dtos.Insurance.Validators;

public class AddExperimentValidator : AbstractValidator<ExperimentDTO>
{
    public AddExperimentValidator()
    {
    }
}
