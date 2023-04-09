using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class InsuranceIdValidator : AbstractValidator<IInsuranceIdDTO>
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IContextResolver _officeResolver;
        public InsuranceIdValidator(IInsuranceRepository insuranceRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _insuranceRepository = insuranceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.InsuranceId)
                .MustAsync(async (insuranceId, token) =>
                {
                    if (insuranceId == null)
                    {
                        return true;
                    }
                    else
                    {
                    return await _insuranceRepository.CheckExistInsuranceId(officeId, insuranceId);
                    }

                }).WithMessage("بیمه ای با این مشخصات موجود نیست.");
        }
    }
}
