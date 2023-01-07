using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashValidator : AbstractValidator<CashesDTO>
{
    public AddCashValidator()
    {
        RuleFor(x => x.Recieved).NotEmpty();
    }
}
