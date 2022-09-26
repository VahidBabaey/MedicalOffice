using FluentValidation;

namespace MedicalOffice.Application.Dtos.Specialization.Validators;

public class AddSpecializationValidator : AbstractValidator<SpecializationDTO>
{
    public AddSpecializationValidator()
    {
    }
}
