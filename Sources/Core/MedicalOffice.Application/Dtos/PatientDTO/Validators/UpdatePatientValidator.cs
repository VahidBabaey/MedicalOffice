using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.PatientDTO.Validators;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDTO>
{
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IContextResolver _officeResolver;
    private readonly IMedicalStaffRepository _medicalStaffrepository;
    public UpdatePatientValidator(IMedicalStaffRepository medicalstaffrepository, IInsuranceRepository insuranceRepository, IContextResolver QueryStringResolver)
    {
        _insuranceRepository = insuranceRepository;
        _medicalStaffrepository = medicalstaffrepository;
        _officeResolver = QueryStringResolver;

        var officeId = _officeResolver.GetOfficeId().Result;

        //Include(new IPhoneNumberListValidator());
        //Include(new NationalIdValidator());

        RuleFor(x => x.FirstName).NotEmpty().Length(1, 100);

        RuleFor(x => x.LastName).NotEmpty().Length(1, 100);

        RuleFor(p => p.TelePhoneNumber)
            .Must(p => IsValidTelePhoneNumberList(p)).WithMessage("{PropertyName} is not valid");

        RuleFor(x => x.InsuranceId)
            .MustAsync(async (insuranceId, token) =>
            {
                if (insuranceId == null)
                {
                    return true;
                }
                else
                {
                    return await _insuranceRepository.CheckExistInsuranceId(officeId, insuranceId);
                }

            }).WithMessage("{PropertyName} isn't exist");

        RuleFor(x => x.ReferrerMedicalStaffId)
            .MustAsync(async (ReferrerMedicalStaffId, token) =>
            {
                if (ReferrerMedicalStaffId == null)
                {
                    return true;
                }
                else
                {
                    return await _medicalStaffrepository.CheckMedicalStaffReferrerExist(ReferrerMedicalStaffId, officeId);
                }
            })
            .WithMessage("{PropertyName} isn't exist");
    }

    bool IsValidTelePhoneNumberList(string[]? telePhoneNumbers)
    {
        Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

        if (telePhoneNumbers != null)
        {
            foreach (string telNumber in telePhoneNumbers)
            {
                if (!regex.IsMatch(telNumber))
                {
                    return false;
                }
            }
            return true;
        }
        return true;
    }
}
