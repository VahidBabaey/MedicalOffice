using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IShiftIdValidator : AbstractValidator<IShiftIdDTO>
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IContextResolver _officeResolver;

        public IShiftIdValidator(IShiftRepository shiftRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _shiftRepository = shiftRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ShiftId)
                .NotEmpty()
                .MustAsync(async (shiftId, token) =>
                {
                    var leaveTypeExists = await _shiftRepository.CheckExistShiftId(officeId, shiftId);
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
