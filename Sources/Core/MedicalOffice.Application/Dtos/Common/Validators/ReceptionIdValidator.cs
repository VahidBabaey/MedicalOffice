using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class ReceptionIdValidator : AbstractValidator<IReceptionIdDTO>
    {
        private readonly IReceptionRepository _receptionRepository;
        private readonly IOfficeResolver _officeResolver;

        public ReceptionIdValidator(IReceptionRepository receptionRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _receptionRepository = receptionRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ReceptionId)
                .NotEmpty()
                .MustAsync(async (receptionId, token) =>
                {
                    var leaveTypeExists = await _receptionRepository.CheckExistReceptionId(officeId, receptionId);
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
