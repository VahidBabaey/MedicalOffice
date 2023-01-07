using FluentValidation;
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

        public SectionIdValidator(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;

            RuleFor(x => x.SectionId)
                .NotEmpty()
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _sectionRepository.CheckExistSectionId(id);
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
