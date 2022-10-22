using FluentValidation;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class AddPatientValidator : AbstractValidator<PatientDTO>
{
    public AddPatientValidator()
    {
    }
}
