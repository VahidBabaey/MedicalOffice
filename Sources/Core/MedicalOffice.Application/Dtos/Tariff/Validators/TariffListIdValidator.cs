using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Tariff.Validators
{
    public class TariffListIdValidator : AbstractValidator<TariffListIdDTO>
    {
        private readonly IServiceTariffRepository _tariffrepository;
        private readonly IContextResolver _officeResolver;

        public TariffListIdValidator(IServiceTariffRepository tariffrepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _tariffrepository = tariffrepository;

            var officeId = _officeResolver.GetOfficeId().Result;


            RuleFor(x => x.TariffId)

                .MustAsync(async (ids, token) =>
                {
                    foreach (var item in ids)
                    {
                        var isExist = await _tariffrepository.CheckExistTariffId(officeId, item);
                        if (!isExist)
                        {
                            return false;
                        }
                    }
                    return true;
                });
        }
    }
}