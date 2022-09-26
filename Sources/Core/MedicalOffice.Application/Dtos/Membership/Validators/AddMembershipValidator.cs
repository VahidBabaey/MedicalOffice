using FluentValidation;

namespace MedicalOffice.Application.Dtos.Membership.Validators;

public class AddMembershipValidator : AbstractValidator<MembershipDTO>
{
    public AddMembershipValidator()
    {
    }
}
