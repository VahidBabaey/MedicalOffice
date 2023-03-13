using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Entities;
using System.Linq;
using System.Numerics;

namespace MedicalOffice.Application.Dtos.MembershipDTO.Validators;

public class AddMembershipValidator : AbstractValidator<MembershipDTO>
{
    private readonly IEnumerable<Membership> _Membership;
    public AddMembershipValidator(IEnumerable<Membership> Membership)
    {
        _Membership = Membership;
        RuleFor(x => x.Name)
            //.Must(IsNameUnique).WithMessage("Name must be unique")
            .Must(IsNameUnique).WithMessage("نام عضویت باید یکتا باشد")
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
            .LessThanOrEqualTo(100)
            .WithMessage("تعداد کاراکترهای کد تخفیف باید برابر یا کمتر از 100 باشد");
    }
    public bool IsNameUnique(MembershipDTO editedMembership, string newValue)
    {
        return _Membership.All(membership =>
          membership.Equals(editedMembership) || membership.Name != newValue);
    }
}
