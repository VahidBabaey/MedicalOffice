using FluentValidation;

namespace MedicalOffice.Application.Dtos.MembershipDTO.Validators;

public class AddMembershipValidator : AbstractValidator<MembershipDTO>
{
    public AddMembershipValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.Discount).NotEmpty();
    }
}
