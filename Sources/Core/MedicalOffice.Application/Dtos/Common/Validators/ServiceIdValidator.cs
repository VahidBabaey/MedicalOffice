﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class ServiceIdValidator : AbstractValidator<IServiceIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRouteResolver _officeResolver;
        public ServiceIdValidator(IServiceRepository serviceRepository, IRouteResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .WithMessage("ورود شناسه خدمت ضروری است")
                .MustAsync(async (serviceId, token) =>
                    {
                        return await _serviceRepository.CheckExistServiceId(officeId, serviceId);

                    })
                //.WithMessage("{PropertyName} isn't exist");
                .WithMessage("شناسه خدمت یافت نشد");
        }
    }
}
