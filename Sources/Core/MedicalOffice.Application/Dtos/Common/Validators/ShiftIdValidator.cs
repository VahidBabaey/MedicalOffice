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
    public class ShiftIdValidator : AbstractValidator<IShiftIdDTO>
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IQueryStringResolver _officeResolver;

        public ShiftIdValidator(IShiftRepository shiftRepository, IQueryStringResolver officeResolver)
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
