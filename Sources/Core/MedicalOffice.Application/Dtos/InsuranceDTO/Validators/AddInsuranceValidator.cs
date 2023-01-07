using FluentValidation;

namespace MedicalOffice.Application.Dtos.InsuranceDTO.Validators;

public class AddInsuranceValidator : AbstractValidator<InsuranceDTO>
{
    public AddInsuranceValidator()
    {

        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.InsuranceCode).NotEmpty();

    }
}
