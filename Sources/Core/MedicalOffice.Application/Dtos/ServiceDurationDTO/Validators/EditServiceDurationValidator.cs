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
    public class EditServiceDurationValidator : AbstractValidator<EditServiceDurationDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;
        private static readonly int minDuration = 1;
        public EditServiceDurationValidator(
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IContextResolver officeResolver
            )
        {
            _medicalStaffRepository = medicalStaffRepository;
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));

            RuleFor(x => x.MedicalStaffId)
                .NotEmpty()
                //.WithMessage(ValidationMessage.Required.For("MedicalStaffId"));
                .WithMessage("ورود شناسه کادر درمان ضروری است");

            RuleFor(x => x.ServiceId)
               .NotEmpty()
               //.WithMessage(ValidationMessage.Required.For("ServiceId"));
               .WithMessage("ورود شناسه خدمت ضروری است");

            RuleFor(x => x.Duration)
               .NotEmpty()
               //.WithMessage(ValidationMessage.Required.For("Duration"))
               .WithMessage("ورود مدت زمان خدمت ضروری است")
               .GreaterThanOrEqualTo(minDuration)
               //.WithMessage(ValidationMessage.GreaterOrEqual.For("Duration", minDuration));
               .WithMessage("مدت زمان خدمت باید بزرگتر یا برابر با 1 باشد");
        }
    }
}
