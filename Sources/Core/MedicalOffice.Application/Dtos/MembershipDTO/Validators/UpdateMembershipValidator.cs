using FluentValidation;

namespace MedicalOffice.Application.Dtos.MembershipDTO.Validators;

public class UpdateMembershipValidator : AbstractValidator<UpdateMembershipDTO>
{
    public UpdateMembershipValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.IsActive).NotNull().Must(x => x == false || x == true);
        RuleFor(x => x.Discount).NotEmpty().LessThanOrEqualTo(100);
    }
}
