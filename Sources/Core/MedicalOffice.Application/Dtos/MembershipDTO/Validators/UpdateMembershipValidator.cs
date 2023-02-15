using FluentValidation;

namespace MedicalOffice.Application.Dtos.MembershipDTO.Validators;

public class UpdateMembershipValidator : AbstractValidator<UpdateMembershipDTO>
{
    public UpdateMembershipValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.IsActive);
        RuleFor(x => x.Discount).NotEmpty();
        RuleFor(x => x.Discount).NotEmpty().LessThanOrEqualTo(100);
    }
}
