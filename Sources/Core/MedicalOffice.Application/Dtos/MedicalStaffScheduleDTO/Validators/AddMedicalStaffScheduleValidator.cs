using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators
{
    public class MedicalStaffScheduleValidator : AbstractValidator<MedicalStaffScheduleDTO>
    {
        public MedicalStaffScheduleValidator()
        {
            RuleForEach(x => x.MedicalStaffSchedule)
                .SetValidator(new MedicalStaffDayScheduleValidator());
        }
    }

    public class MedicalStaffDayScheduleValidator : AbstractValidator<MedicalStaffDaySchedule>
    {
        public MedicalStaffDayScheduleValidator()
        {
            #region MorningStartValidation
            RuleFor(x => x.MorningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("MorningStart"));
            RuleFor(x => TimeOnly.Parse(x.MorningStart))
                .LessThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.LessThan.For("MorningStart", "MorningEnd"));
            #endregion

            #region MorningEndValidation
            RuleFor(x => x.MorningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("MorningEnd"));
            RuleFor(x => TimeOnly.Parse(x.MorningEnd))
                .GreaterThan(x => TimeOnly.Parse(x.MorningStart))
                .WithMessage(ValidationMessage.GreaterThan.For("MorningEnd", "MorningStart"))
                .LessThan(x => TimeOnly.Parse(x.EveningStart))
                .WithMessage(ValidationMessage.LessThan.For("MorningEnd", "EveningStart"));
            #endregion

            #region EveningStartValidation
            RuleFor(x => x.EveningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("EveningStart"));
            RuleFor(x => TimeOnly.Parse(x.EveningStart))
                .GreaterThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.GreaterThan.For("EveningStart", "MorningEnd"))
                .LessThan(x => TimeOnly.Parse(x.EveningEnd))
                .WithMessage(ValidationMessage.LessThan.For("EveningStart","EveningEnd"));
            #endregion

            #region EveningEndValidation
            RuleFor(x => x.EveningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("EveningEnd"));

            RuleFor(x => TimeOnly.Parse(x.EveningEnd))
                .GreaterThan(x => TimeOnly.Parse(x.EveningStart))
                .WithMessage(ValidationMessage.GreaterThan.For("EveningEnd", "EveningStart"));
            #endregion

        }
    }
}

