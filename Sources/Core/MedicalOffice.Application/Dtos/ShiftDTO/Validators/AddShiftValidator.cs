using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ShiftDTO.Validators;

public class AddShiftValidator : AbstractValidator<ShiftDTO>
{
    public AddShiftValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.StartTime).NotEmpty()
        .WithMessage(ValidationMessage.Required.For("StartTime"));

        When(x => TimeOnly.TryParse(x.StartTime, out TimeOnly result), () =>
        {
            RuleFor(x => TimeOnly.Parse(x.StartTime))
                .LessThanOrEqualTo(x => TimeOnly.Parse(x.EndTime))
                    .WithMessage(ValidationMessage.LessOrEqual.For("StartTime", "EndTime"))
                .LessThan(x => TimeOnly.Parse(x.EndTime))
                    .WithMessage(ValidationMessage.LessThan.For("StartTime", "EndTime"));
        }).Otherwise(() =>
        {
            RuleFor(x => x.StartTime)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("StartTime"));
        });

        RuleFor(x => x.EndTime)
            .NotEmpty()
                .WithMessage(ValidationMessage.Required.For("EndTime"));
            

        When(x => TimeOnly.TryParse(x.EndTime, out TimeOnly result), () =>
        {
            RuleFor(x => TimeOnly.Parse(x.EndTime))
                .GreaterThanOrEqualTo(x => TimeOnly.Parse(x.StartTime))
                    .WithMessage(ValidationMessage.GreaterOrEqual.For("EndTime", "StartTime"))
                .GreaterThan(x => TimeOnly.Parse(x.StartTime))
                    .WithMessage(ValidationMessage.GreaterThan.For("EndTime", "StartTime"));
        }).Otherwise(() =>
        {
            RuleFor(x => x.EndTime)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                .WithMessage(ValidationMessage.NotValid.For("EndTime"));
        });
    }
}
