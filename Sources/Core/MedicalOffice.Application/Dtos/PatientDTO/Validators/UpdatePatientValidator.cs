using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDTO>
{
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IQueryStringResolver _officeResolver;
    public UpdatePatientValidator(IInsuranceRepository insuranceRepository, IQueryStringResolver officeResolver)
    {
        _insuranceRepository = insuranceRepository;
        _officeResolver = officeResolver;

        RuleFor(x => x.FirstName).NotEmpty().Length(1, 100);
        RuleFor(x => x.LastName).NotEmpty().Length(1, 100);
        Include(new IPhoneNumberListValidator());
        Include(new ITelePhoneNumberListValidator());
        Include(new NationalIdValidator());
        Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));
    }
}
