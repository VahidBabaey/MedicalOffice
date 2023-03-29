using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class CashIdValidator : AbstractValidator<ICashIdDTO>
    {
        private readonly ICashRepository _cashRepository;
        private readonly IRouteResolver _officeResolver;

        public CashIdValidator(ICashRepository cashRepository, IRouteResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _cashRepository = cashRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.CashId)
                .NotEmpty()
                .WithMessage("ورود شناسه صندوق ضروری است")
                .MustAsync(async (cashId, token) =>
                {
                    var leaveTypeExists = await _cashRepository.CheckExistCashId(officeId, cashId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                //.WithMessage("{PropertyName} isn't exist");
                .WithMessage("شناسه صندوق مورد نظر موجود نیست");
        }
    }
}
