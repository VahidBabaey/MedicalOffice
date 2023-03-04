using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceDurationDTO.Validators
{
    public class AddServiceDurationValidator : AbstractValidator<ServiceDurationDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IQueryStringResolver _officeResolver;
        private static readonly int minDuration = 1;
        public AddServiceDurationValidator(
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IQueryStringResolver officeResolver
            )
        {
            _medicalStaffRepository = medicalStaffRepository;
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));

            RuleFor(x => x.MedicalStaffId)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required.For("MedicalStaffId"));

            RuleFor(x => x.ServiceId)
               .NotEmpty()
               .WithMessage(ValidationMessage.Required.For("ServiceId"));

            RuleFor(x => x.Duration)
               .NotEmpty()
               .WithMessage(ValidationMessage.Required.For("Duration"))
               .GreaterThanOrEqualTo(minDuration)
               .WithMessage(ValidationMessage.GreaterOrEqual.For("Duration", minDuration));
        }
    }
}
