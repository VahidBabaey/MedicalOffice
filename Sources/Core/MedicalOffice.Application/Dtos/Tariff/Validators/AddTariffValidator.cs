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

            RuleFor(x => x.TariffValue).NotEmpty();
            RuleFor(x => x.InternalTariffValue).NotEmpty();
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));
            Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));
        }
    }
}
