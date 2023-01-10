using FluentValidation;
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
    public class ServiceListIdValidator : AbstractValidator<IServiceListIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IOfficeResolver _officeResolver;
        public ServiceListIdValidator(IServiceRepository serviceRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .MustAsync(async (serviceId, token) =>
                    {
                        return await _serviceRepository.CheckExistServiceListId(officeId, serviceId);

                    })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
