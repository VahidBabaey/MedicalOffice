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
    public class IServiceIdValidator : AbstractValidator<IServiceIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;
        public IServiceIdValidator(IServiceRepository serviceRepository, IContextResolver officeResolver)
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
