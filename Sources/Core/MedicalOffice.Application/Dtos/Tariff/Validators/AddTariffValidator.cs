using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators;
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
        private readonly IContextResolver _officeResolver;
        private readonly IServiceTariffRepository _serviceTariffRepository;

        public AddTariffValidator(IContextResolver officeResolver, IServiceRepository serviceRepository, IInsuranceRepository insuranceRepository, IServiceTariffRepository serviceTariffRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _insuranceRepository = insuranceRepository;
            _serviceTariffRepository = serviceTariffRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));

            RuleFor(x => x.ServiceId)
                .MustAsync(async (serviceId, token) =>
                {
                    return await _serviceRepository.isTariffValid(serviceId);
                });

            Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));

            RuleFor(x => x.InternalTariffValue)
                .NotEmpty()
                .WithMessage("ورود تعرفه داخلی ضروری است");

            RuleFor(x => x.TariffValue)
                .NotEmpty()
                .WithMessage("ورود مبلغ تعرفه ضروری است");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("مبلغ تخفیف باید بین 0 تا 100 کاراکتر داشته باشد");

            RuleFor(x => x.InsurancePercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("درصد بیمه باید بین 0 تا 100 کاراکتر داشته باشد");

            RuleFor(x => x)
                .MustAsync(async (x, token) =>
                {
                    if (x.InsuranceId != null)
                    {
                        return await _serviceTariffRepository.IsUniqInsuranceTariff(x.InsuranceId, x.ServiceId, officeId);
                    }
                    return true;
                })
                //.WithMessage("There is already a tariff with this insuranceId");
                .WithMessage("قبلا تعرفه ای با این آیدی بیمه تعریف شده");
        }
    }
}