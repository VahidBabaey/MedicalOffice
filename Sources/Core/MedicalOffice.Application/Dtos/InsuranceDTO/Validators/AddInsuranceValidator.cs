using FluentValidation;
using System.Numerics;

namespace MedicalOffice.Application.Dtos.InsuranceDTO.Validators;

public class AddInsuranceValidator : AbstractValidator<InsuranceDTO>
{
    public AddInsuranceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام بیمه ضروری است")
            .Length(1, 100)
            .WithMessage("نام بیمه باید بین 1 تا 100 کاراکتر داشته باشد");
        RuleFor(x => x.InsurancePercent)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .WithMessage("درصد سازمان عددی در بازه 0 تا 100 است.");
    }
}