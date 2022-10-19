using FluentValidation;
using MedicalOffice.Application.Dtos.ExperimentDTO;

namespace MedicalOffice.Application.Dtos.ExperimentDTO.Validators;

public class AddExperimentValidator : AbstractValidator<ExperimentDTO>
{
    public AddExperimentValidator()
    {
    }
}
