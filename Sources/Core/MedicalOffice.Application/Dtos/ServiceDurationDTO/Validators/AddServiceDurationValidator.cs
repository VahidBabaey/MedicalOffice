using FluentValidation;
using MedicalOffice.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceDurationDTO.Validators
{
    public class ServiceDurationValidator : AbstractValidator<ServiceDurationDTO>
    {
        private static readonly int minDuration = 1;
        public ServiceDurationValidator()
        {
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
