using FluentValidation;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class AddPatientValidator : AbstractValidator<PatientDTO>
{
    public AddPatientValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().Length(1, 100);
        RuleFor(x => x.LastName).NotEmpty().Length(1, 100);
        Include(new IPhoneNumberListValidator());
        Include(new ITelePhoneNumberListValidator());
        Include(new NationalIdValidator());
    }
}
