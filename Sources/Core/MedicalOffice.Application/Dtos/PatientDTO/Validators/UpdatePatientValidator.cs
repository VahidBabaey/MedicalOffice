using FluentValidation;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDTO>
{
    public UpdatePatientValidator()
    {
    }
}
