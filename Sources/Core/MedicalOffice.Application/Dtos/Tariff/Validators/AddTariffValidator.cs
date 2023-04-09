using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
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

            Include(new IServiceIdValidator(_serviceRepository, _officeResolver));

            RuleFor(x => x.ServiceId)
                .MustAsync(async (serviceId, token) =>
                {
                    return await _serviceRepository.isTariffValid(serviceId);
                });

            Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));

            RuleFor(x => x.InternalTariffValue)
                .NotEmpty()
                .WithMessage("ورود تعرفه داخلی ضروری است");

            RuleFor(x => x)
                .Must((x) =>
                {
                    var diff = Math.Abs(x.TariffValue - x.InternalTariffValue);
                    if (x.Difference == diff)
                        return true;
                    else return false;
                })
                .When(x => x.Difference != null)
                .WithMessage(" ما به اتفاوت باید با تفاوت تعرفه و تعرفه داخلی برابر باشد. ");

            RuleFor(x => x.TariffValue)
                .NotEmpty()
                .WithMessage("ورود مبلغ تعرفه ضروری است");

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