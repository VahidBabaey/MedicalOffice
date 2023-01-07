using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Commons;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    private readonly ISectionRepository _sectionRepository;
    public AddServiceValidator(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.GenericCode).NotEmpty();
        Include(new SectionIdValidator(_sectionRepository));
    }
}
