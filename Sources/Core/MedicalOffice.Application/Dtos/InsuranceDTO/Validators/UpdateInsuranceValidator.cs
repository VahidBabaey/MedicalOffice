using FluentValidation;

namespace MedicalOffice.Application.Dtos.InsuranceDTO.Validators;

public class UpdateInsuranceValidator : AbstractValidator<UpdateInsuranceDTO>
{
    public UpdateInsuranceValidator()
    {

        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.InsuranceCode).NotEmpty();

    }
}
