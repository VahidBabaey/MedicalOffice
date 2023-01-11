using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDTO>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(1, 100);
        RuleFor(x => x.LastName).NotEmpty().Length(1, 100);
        Include(new IPhoneNumberListValidator());
        Include(new ITelePhoneNumberListValidator());
    }
}
