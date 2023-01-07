using FluentValidation;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class AddPatientCommitmentsFormValidator : AbstractValidator<AddPatientCommitmentsFormDTO>
{
    public AddPatientCommitmentsFormValidator()
    {
        RuleFor(x => x.CommitmentName).NotEmpty().Length(1, 200);
        RuleFor(x => x.DateSolar).NotEmpty().Length(1, 200);
        RuleFor(x => x.DateAD).NotEmpty();
    }
}
