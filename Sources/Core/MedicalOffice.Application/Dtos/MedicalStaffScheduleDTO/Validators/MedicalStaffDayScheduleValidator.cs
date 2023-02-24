using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators
{
    public class MedicalStaffDayScheduleValidator : AbstractValidator<MedicalStaffDaySchedule>
    {
        public MedicalStaffDayScheduleValidator()
        {
            #region MorningStartValidation
            RuleFor(x => x.MorningStart)
                .NotEmpty()
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("MorningStart"));

            RuleFor(x => TimeOnly.Parse(x.MorningStart))
                .LessThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.LessThan.For("MorningStart", "MorningEnd"));
            #endregion

            #region MorningEndValidation
            RuleFor(x => x.MorningEnd)
                .NotEmpty()
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
                .NotEmpty()
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("EveningStart"));

            RuleFor(x => TimeOnly.Parse(x.EveningStart))
                .GreaterThan(x => TimeOnly.Parse(x.MorningEnd))
                .WithMessage(ValidationMessage.GreaterThan.For("EveningStart", "MorningEnd"))
                .LessThan(x => TimeOnly.Parse(x.EveningEnd))
                .WithMessage(ValidationMessage.LessThan.For("EveningStart", "EveningEnd"));
            #endregion

            #region EveningEndValidation
            RuleFor(x => x.EveningEnd)
                .NotEmpty()
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("EveningEnd"));

            RuleFor(x => TimeOnly.Parse(x.EveningEnd))
                .GreaterThan(x => TimeOnly.Parse(x.EveningStart))
                .WithMessage(ValidationMessage.GreaterThan.For("EveningEnd", "EveningStart"));
            #endregion

        }
    }
}

