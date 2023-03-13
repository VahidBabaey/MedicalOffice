using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.ShiftDTO.Validators;

public class AddShiftValidator : AbstractValidator<ShiftDTO>
{
    public AddShiftValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود عنوان شیفت ضروری است")
            .Length(1, 100)
            .WithMessage("عنوان شیفت باید بین 1 تا 100 کاراکتر باشد");
        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("ورود زمان شیفت ضروری است");
        //.WithMessage(ValidationMessage.Required.For("StartTime"));

        When(x => TimeOnly.TryParse(x.StartTime, out TimeOnly result), () =>
        {
            RuleFor(x => TimeOnly.Parse(x.StartTime))
                .LessThanOrEqualTo(x => TimeOnly.Parse(x.EndTime))
                    //.WithMessage(ValidationMessage.LessOrEqual.For("StartTime", "EndTime"));
                    .WithMessage("زمان شروع شیفت باید قبل یا همزمان با پایان شیفت باشد");
        }).Otherwise(() =>
        {
            RuleFor(x => x.StartTime)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                //.WithMessage(ValidationMessage.NotValid.For("StartTime"));
                .WithMessage("زمان شروع معتبر نیست");
        });

        RuleFor(x => x.EndTime)
            .NotEmpty()
                //.WithMessage(ValidationMessage.Required.For("EndTime"));
                .WithMessage("ورود زمان پایان ضروری است");


        When(x => TimeOnly.TryParse(x.EndTime, out TimeOnly result), () =>
        {
            RuleFor(x => TimeOnly.Parse(x.EndTime))
                .GreaterThanOrEqualTo(x => TimeOnly.Parse(x.StartTime))
                    //.WithMessage(ValidationMessage.GreaterOrEqual.For("EndTime", "StartTime"));
                    .WithMessage("زمان پایان شیفت باید بعد یا همزمان با آغاز شیفت باشد");
        }).Otherwise(() =>
        {
            RuleFor(x => x.EndTime)
                .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                //.WithMessage(ValidationMessage.NotValid.For("EndTime"));
                .WithMessage("زمان پایان معتبر نیست");
        });
    }
}
