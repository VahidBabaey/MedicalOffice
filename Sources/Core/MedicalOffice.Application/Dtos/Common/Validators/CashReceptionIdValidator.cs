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
    public class CashReceptionIdValidator : AbstractValidator<IReceptionIdDTO>
    {
        private readonly ICashRepository _cashRepository;
        private readonly IQueryStringResolver _officeResolver;

        public CashReceptionIdValidator(ICashRepository cashRepository, IQueryStringResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _cashRepository = cashRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ReceptionId)
                .NotEmpty()
                .MustAsync(async (receptionId, token) =>
                {
                    var leaveTypeExists = await _cashRepository.CheckExistReceptionId(officeId, receptionId);
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
