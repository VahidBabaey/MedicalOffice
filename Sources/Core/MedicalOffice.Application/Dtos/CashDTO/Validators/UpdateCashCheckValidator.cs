using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class UpdateCashCheckValidator : AbstractValidator<UpdateCashCheckDTO>
{
    public UpdateCashCheckValidator()
    {
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.AccountNumber).NotEmpty().Length(1, 13);
        RuleFor(x => x.Date).NotEmpty();
    }
}
