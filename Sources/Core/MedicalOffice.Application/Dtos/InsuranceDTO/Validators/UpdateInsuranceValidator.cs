using FluentValidation;

namespace MedicalOffice.Application.Dtos.InsuranceDTO.Validators;

public class UpdateInsuranceValidator : AbstractValidator<UpdateInsuranceDTO>
{
    public UpdateInsuranceValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام بیمه ضروری است")
            .Length(1, 100)
            .WithMessage("نام بیمه باید بین 1 تا 100 کاراکتر داشته باشد");
        RuleFor(x => x.InsuranceCode)
            .NotEmpty()
            .WithMessage("رود کد بیمه ضروری است");

    }
}
