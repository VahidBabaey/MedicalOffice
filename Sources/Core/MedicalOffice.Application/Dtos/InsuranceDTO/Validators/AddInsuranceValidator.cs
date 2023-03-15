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
        RuleFor(x => x.InsuranceCode)
            .NotEmpty()
            .WithMessage("ورود کد بیمه ضروری است");
        RuleFor(x => x.InsurancePercent)
            .ExclusiveBetween(1, 100)
            .WithMessage("لطفا عددی در بازه 1 تا 100 انتخاب کنید.");
    }

}
