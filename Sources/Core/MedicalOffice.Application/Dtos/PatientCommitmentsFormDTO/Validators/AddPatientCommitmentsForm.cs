using FluentValidation;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class AddPatientCommitmentsForm : AbstractValidator<AddPatientCommitmentsFormDTO>
{
    public AddPatientCommitmentsForm()
    {
    }
}
