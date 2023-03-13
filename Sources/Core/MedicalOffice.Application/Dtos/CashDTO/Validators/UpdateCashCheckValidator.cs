using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class UpdateCashCheckValidator : AbstractValidator<UpdateCashCheckDTO>
{
    public UpdateCashCheckValidator()
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage("ورود شماره حساب ضروری است")
            .Length(1, 13)
            .WithMessage("شماره حساب باید بین 1 تا 13 کاراکتر داشته باشد");
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("ورود تاریخ ضروری است");
    }
}
