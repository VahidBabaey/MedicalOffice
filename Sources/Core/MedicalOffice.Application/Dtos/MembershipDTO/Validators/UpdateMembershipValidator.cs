using FluentValidation;

namespace MedicalOffice.Application.Dtos.MembershipDTO.Validators;

public class UpdateMembershipValidator : AbstractValidator<UpdateMembershipDTO>
{
    public UpdateMembershipValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام عضویت ضروری است")
            .Length(1, 100)
            .WithMessage("نام عضویت باید بین 1 تا 100 کاراکتر داشته باشد");
        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("نام عضویت نباید بدون مقدار باشد")
            .Must(x => x == false || x == true);
        RuleFor(x => x.Discount)
            .NotEmpty()
            .WithMessage("ورود درصد تخفیف ضروری است")
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("مقدار کد تخفیف باید برابر یا کمتر از 100 باشد");
    }
}
