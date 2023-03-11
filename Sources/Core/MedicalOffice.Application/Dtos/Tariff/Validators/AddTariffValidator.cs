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
        private readonly IQueryStringResolver _officeResolver;
        private readonly IServiceTariffRepository _serviceTariffRepository;

        public AddTariffValidator(IQueryStringResolver officeResolver, IServiceRepository serviceRepository, IInsuranceRepository insuranceRepository, IServiceTariffRepository serviceTariffRepository)
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

            RuleForEach(x => x.Tariffs)
                .SetValidator(new AddTariffListValidator(_officeResolver, _insuranceRepository));

            RuleFor(x => x)
                .MustAsync(async (x, token) =>
                {
                    if (x.Tariffs.Count != 0)
                    {
                        foreach (var item in x.Tariffs)
                        {
                            if (item.InsuranceId != null)
                            {
                                return await _serviceTariffRepository.IsUniqInsuranceTariff(item.InsuranceId, x.ServiceId, officeId);
                            }
                        }

                        return true;
                    }
                    return true;
                })
                .WithMessage("There is already a tariff with this insuranceId");
        }
    }
}