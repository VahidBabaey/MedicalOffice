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
                    //.WithMessage("The MorningStart or MorningEnd isn't exist")
                    .WithMessage("ساعت شروع شیفت صبح یا ساعت پایان شیفت صبح موجود نیست")
                .Must(x => IsNullabalityValid(x.EveningStart, x.EveningEnd))
                    //.WithMessage("The EveningStart or EveningEnd isn't exist")
                    .WithMessage("ساعت شروع شیفت عصر یا ساعت پایان شیفت عصر موجود نیست")
                .Must(x => IsConvertbleToTimeOnly(x.MorningStart, x.MorningEnd))
                    //.WithMessage("The MorningStart should be less than MorningEnd")
                    .WithMessage("ساعت شروع شیفت صبح باید قبل از ساعت پایان شیفت صبح باشد")
                .Must(x => IsConvertbleToTimeOnly(x.MorningStart, x.MorningEnd))
                    //.WithMessage("The EveningStart should be less than EveningEnd")
                    .WithMessage("ساغت شروع شیفت عصر باید قبل از ساعت پایان شیفت عصر باشد")
                .Must(x => IsConvertbleToTimeOnly(x.MorningEnd, x.EveningStart))
                    //.WithMessage("The MorningEnd should be less than EveningStart");
                    .WithMessage("ساعت پایان شیفت صبح باید قبل از ساعت شروع شیفت عصر باشد");
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