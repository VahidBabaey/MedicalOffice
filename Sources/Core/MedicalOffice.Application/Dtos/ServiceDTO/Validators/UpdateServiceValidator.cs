using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceDTO>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IContextResolver _officeResolver;
    public UpdateServiceValidator(IServiceRepository serviceRepository, ISectionRepository sectionRepository, IContextResolver officeResolver)
    {
        _serviceRepository = serviceRepository;
        _officeResolver = officeResolver;
        _sectionRepository = sectionRepository;
        
        var officeId = _officeResolver.GetOfficeId().Result;
        
        Include(new ISectionIdValidator(_sectionRepository, _officeResolver));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام خدمت ضروری است")
            .Length(1, 200)
            .WithMessage("نام خدمت باید بین 1 تا 200 کاراکتر داشته باشد");
        RuleFor(x => x.CalculationMethod)
            .NotEmpty()
            .WithMessage("ورود نحوه محاسبه خدمت ضروری است");
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ورود شناسه خدمت ضروری است")
            .MustAsync(async (serviceId, token) =>
            {
                return await _serviceRepository.CheckExistServiceId(officeId, serviceId);

            })
            //.WithMessage("{PropertyName} isn't exist");
            .WithMessage("شناسه خدمت موجود نیست");
    }
}
