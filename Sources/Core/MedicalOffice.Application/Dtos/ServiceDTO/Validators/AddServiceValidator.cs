using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Commons;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddServiceValidator(ISectionRepository sectionRepository, IQueryStringResolver officeResolver)
    {
        _officeResolver = officeResolver;
        _sectionRepository = sectionRepository;

        Include(new SectionIdValidator(_sectionRepository, _officeResolver));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام خدمت ضروری است")
            .Length(1, 200)
            .WithMessage("نام خدمت باید بین 1 تا 200 کاراکتر داشته باشد");
        RuleFor(x => x.CalculationMethod)
            .NotEmpty()
            .WithMessage("ورود نحوه محاسبه قیمت ضروری است");
    }
}