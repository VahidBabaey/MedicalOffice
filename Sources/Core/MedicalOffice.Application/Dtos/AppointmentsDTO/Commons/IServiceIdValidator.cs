using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Commons
{
    public class IServiceIdValidator:AbstractValidator<IServiceIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;

        public IServiceIdValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must(x => isServiceExist(x))
                .WithMessage("{PropertyName} isn't exist");
        }

        private bool isServiceExist(Guid serviceId)
        {
            var existingService = _serviceRepository.GetById(serviceId);

            if (existingService!=null)
            {
                return true;
            }

            return false;
        }
    }
}
