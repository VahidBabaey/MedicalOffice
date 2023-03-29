using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Domain.Entities;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class ServiceIdsValidator : AbstractValidator<IServiceIdsDTO>
    {
        private readonly IRouteResolver _officeResolver;
        private readonly IServiceRepository _serviceRepository;

        public ServiceIdsValidator(IRouteResolver officeResolver, IServiceRepository serviceRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ServiceIds)
                .NotEmpty()
                .WithMessage("ورود شناسه خدمت ضروری است")
                .MustAsync(async (serviceIds, token) =>
                {
                    foreach (var item in serviceIds)
                    {
                        var isServiceExist = await _serviceRepository.CheckExistServiceId(officeId, item);
                        if (!isServiceExist)
                        {
                            return false;
                        }
                    }
                    return true;
                })
                //.WithMessage("{PropertyName} has some invalid values");
                .WithMessage("شناسه خدمت دارای مقدارهای نامعتبر میباشد");
        }
    }
}
