using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class UpdateCashCartValidator : AbstractValidator<UpdateCashCartDTO>
{
    public UpdateCashCartValidator()
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
        RuleFor(x => x.CartNumber)
            .NotEmpty()
            .WithMessage(" ورود شماره کارت ضروری است")
            .Length(1, 16)
            .WithMessage("شماره کارت باید بین 1 تا 16 رقم باشد");
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("ورود تاریخ ضروری است");
    }
}
