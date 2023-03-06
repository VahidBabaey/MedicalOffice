using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Tariff.Validators
{
    public class AddTariffValidator : AbstractValidator<TariffDTO>
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IQueryStringResolver _officeResolver;
        public AddTariffValidator(IQueryStringResolver officeResolver, IServiceRepository serviceRepository, IInsuranceRepository insuranceRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _insuranceRepository = insuranceRepository;

            RuleFor(x => x.Difference)
                .Equal(x => Math.Abs(x.InternalTariffValue - x.TariffValue))
                .When(x => x.InternalTariffValue != default || x.TariffValue != default)
                .WithMessage("{PropertyName} should be the subtraction of internalTariffValue and TariffValue");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
            RuleFor(x => x.InsurancePercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));
            Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));
        }
    }
}
