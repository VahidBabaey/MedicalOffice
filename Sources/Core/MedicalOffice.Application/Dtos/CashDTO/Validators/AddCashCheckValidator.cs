using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashCheckValidator : AbstractValidator<CashCheckDTO>
{
    public AddCashCheckValidator()
    {
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.AccountNumber).NotEmpty().Length(1, 13);
        RuleFor(x => x.Date).NotEmpty();
    }
}
