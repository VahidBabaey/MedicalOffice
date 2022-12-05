using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common;
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
            #region MorningStartValidation
            RuleFor(x => x.MorningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For<MedicalStaffWorkHour>(p => p.MorningStart));
            RuleFor(x => TimeOnly.Parse(x.MorningStart))
                .LessThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.LessThan.For<MedicalStaffWorkHour>(p => p.MorningStart, x => x.MorningEnd));
            #endregion

            #region MorningEndValidation
            RuleFor(x => x.MorningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For<MedicalStaffWorkHour>(p => p.MorningEnd));
            RuleFor(x => TimeOnly.Parse(x.MorningEnd))
                .GreaterThan(x => TimeOnly.Parse(x.MorningStart))
                .WithMessage(ValidationMessage.GreaterThan.For<MedicalStaffWorkHour>(p => p.MorningEnd, x => x.MorningStart))
                .LessThan(x => TimeOnly.Parse(x.EveningStart))
                .WithMessage(ValidationMessage.LessThan.For<MedicalStaffWorkHour>(p => p.MorningEnd, x => x.EveningStart));
            #endregion

            #region EveningStartValidation
            RuleFor(x => x.EveningStart)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For<MedicalStaffWorkHour>(p => p.EveningStart));
            RuleFor(x => TimeOnly.Parse(x.EveningStart))
                .GreaterThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.GreaterThan.For<MedicalStaffWorkHour>(p => p.EveningStart, x => x.MorningEnd))
                .LessThan(x => TimeOnly.Parse(x.EveningEnd))
                .WithMessage(ValidationMessage.LessThan.For<MedicalStaffWorkHour>(p => p.EveningStart, x => x.EveningEnd));
            #endregion

            #region EveningEndValidation
            RuleFor(x => x.EveningEnd)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For<MedicalStaffWorkHour>(p => p.EveningEnd));
            RuleFor(x => TimeOnly.Parse(x.EveningEnd))
                .GreaterThan(x => TimeOnly.Parse(x.EveningStart))
                .WithMessage(ValidationMessage.GreaterThan.For<MedicalStaffWorkHour>(p => p.EveningEnd, x => x.EveningStart));
            #endregion

        }
    }
}

