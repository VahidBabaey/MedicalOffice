using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Entities;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Tariff.Validators
{
    public class AddTariffListValidator : AbstractValidator<TariffList>
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IQueryStringResolver _officeResolver;

        public AddTariffListValidator(IQueryStringResolver officeResolver, IInsuranceRepository insuranceRepository)
        {
            _officeResolver = officeResolver;
            _insuranceRepository = insuranceRepository;

            Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));

            RuleFor(x => x.InternalTariffValue)
                .NotEmpty();

            RuleFor(x => x.TariffValue)
                .NotEmpty();

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.InsurancePercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }
}
