using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class AddPatientCommitmentsFormValidator : AbstractValidator<AddPatientCommitmentsFormDTO>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IOfficeResolver _officeResolver;
    public AddPatientCommitmentsFormValidator(IPatientRepository patientRepository, IOfficeResolver officeResolver)
    {
        _patientRepository = patientRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.CommitmentName).NotEmpty().Length(1, 200);
        RuleFor(x => x.DateSolar).NotEmpty().Length(1, 200);
        RuleFor(x => x.DateAD).NotEmpty();
        Include(new PatientIdValidator(_patientRepository, _officeResolver));
    }
}
