using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class SectionIdValidator : AbstractValidator<ISectionIdDTO>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IOfficeResolver _officeResolver;

        public SectionIdValidator(ISectionRepository sectionRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _sectionRepository = sectionRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .MustAsync(async (sectionId, token) =>
                {
                    var leaveTypeExists = await _sectionRepository.CheckExistSectionId(officeId, sectionId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
