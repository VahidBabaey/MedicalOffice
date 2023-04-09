using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class ISectionIdValidator : AbstractValidator<ISectionIdDTO>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IContextResolver _officeResolver;

        public ISectionIdValidator(ISectionRepository sectionRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _sectionRepository = sectionRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .MustAsync(async (sectionId, token) =>
                {
                    var sectionIdExists = await _sectionRepository.CheckExistSectionId(officeId, sectionId);
                    if (sectionIdExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
