using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators
{
    public class MedicalStaffDayScheduleValidator : AbstractValidator<MedicalStaffDaySchedule>
    {
        public MedicalStaffDayScheduleValidator()
        {
            RuleFor(obj => obj)
                .Must(x => IsNullabalityValid(x.MorningStart, x.MorningEnd))
                    .WithMessage("The MorningStart or MorningEnd isn't exist")
                .Must(x => IsNullabalityValid(x.EveningStart, x.EveningEnd))
                    .WithMessage("The EveningStart or EveningEnd isn't exist")
                .Must(x => IsConvertbleToTimeOnly(x.MorningStart, x.MorningEnd))
                    .WithMessage("The MorningStart should be less than MorningEnd")
                .Must(x => IsConvertbleToTimeOnly(x.MorningStart, x.MorningEnd))
                    .WithMessage("The EveningStart should be less than EveningEnd")
                .Must(x => IsConvertbleToTimeOnly(x.MorningEnd, x.EveningStart))
                    .WithMessage("The MorningEnd should be less than EveningStart");
        }

        private bool IsNullabalityValid(string? a, string? b)
        {
            if ((a != null && b == null) || (b != null && a == null))
            {
                return false;
            }
            return true;
        }

        private bool IsConvertbleToTimeOnly(string? a, string? b)
        {
            if (a != null && b != null)
            {
                var aIsValid = Task.FromResult(TimeOnly.TryParse(a, out TimeOnly aResult)).Result;
                var bIsValid = Task.FromResult(TimeOnly.TryParse(a, out TimeOnly bResult)).Result;
                if (!aIsValid || !bIsValid)
                {
                    return false;
                }

                if (TimeOnly.Parse(a) > TimeOnly.Parse(b))
                {
                    return false;
                }
            }
            return true;
        }
    }
}