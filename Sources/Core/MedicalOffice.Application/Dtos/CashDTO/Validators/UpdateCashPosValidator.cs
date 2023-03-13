using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class UpdateCashPosValidator : AbstractValidator<UpdateCashPosDTO>
{
    public UpdateCashPosValidator()
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
    }
}
