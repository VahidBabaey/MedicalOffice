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
        public ServiceDurationValidator()
        {
            RuleFor(x => x.MedicalStaffId)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.ServiceId)
               .NotEmpty()
               .WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Duration)
               .NotEmpty()
               .WithMessage(ValidationErrorMessages.NotEmpty)
               .GreaterThanOrEqualTo(1)
               .WithMessage($"{ValidationErrorMessages.GreaterOrEqual} '1'");
        }
    }
}
