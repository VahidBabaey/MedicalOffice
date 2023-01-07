using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class UpdateCashCartValidator : AbstractValidator<UpdateCashCartDTO>
{
    public UpdateCashCartValidator()
    {
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.CartNumber).NotEmpty().Length(1, 16);
        RuleFor(x => x.Date).NotEmpty();
    }
}
