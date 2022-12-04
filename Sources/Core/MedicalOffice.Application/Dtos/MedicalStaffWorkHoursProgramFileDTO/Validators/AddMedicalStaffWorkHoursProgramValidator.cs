using FluentValidation;
using MedicalOffice.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO.Validators
{
    public class MedicalStaffWorkHoursProgramValidator : AbstractValidator<MedicalStaffWorkHoursProgramDTO>
    {
        public MedicalStaffWorkHoursProgramValidator()
        {
            RuleForEach(x => x.MedicalStaffWorkHours)
                .SetValidator(new MedicalStaffWorkHourValidator());
        }
    }

    public class MedicalStaffWorkHourValidator : AbstractValidator<MedicalStaffWorkHour>
    {
        public MedicalStaffWorkHourValidator()
        {
            RuleFor(x => x.MorningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationErrorMessages.TimeOnlyPattern);
            RuleFor(x => TimeOnly.Parse(x.MorningStart))
                .LessThan(x => TimeOnly.Parse(x.MorningEnd));

            RuleFor(x => x.MorningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationErrorMessages.TimeOnlyPattern);
            RuleFor(x => TimeOnly.Parse(x.MorningEnd))
             .GreaterThan(x => TimeOnly.Parse(x.MorningStart))
             .LessThan(x => TimeOnly.Parse(x.EveningStart));

            RuleFor(x => x.EveningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationErrorMessages.TimeOnlyPattern);
            RuleFor(x => TimeOnly.Parse(x.EveningStart))
             .GreaterThan(x => TimeOnly.Parse(x.MorningEnd))
             .LessThan(x => TimeOnly.Parse(x.EveningEnd));

            RuleFor(x => x.EveningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationErrorMessages.TimeOnlyPattern);
            RuleFor(x => TimeOnly.Parse(x.EveningEnd))
             .GreaterThan(x => TimeOnly.Parse(x.EveningStart));
        }
    }
}

