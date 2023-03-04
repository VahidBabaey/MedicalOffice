using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceDTO>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IQueryStringResolver _officeResolver;
    public UpdateServiceValidator(IServiceRepository serviceRepository, ISectionRepository sectionRepository, IQueryStringResolver officeResolver)
    {
        _serviceRepository = serviceRepository;
        _officeResolver = officeResolver;
        _sectionRepository = sectionRepository;
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.GenericCode).NotEmpty();
        Include(new SectionIdValidator(_sectionRepository, _officeResolver));
    }
}
