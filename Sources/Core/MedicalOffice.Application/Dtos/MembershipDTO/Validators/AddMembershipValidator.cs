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
        RuleFor(x => x.Name).Must(IsNameUnique).WithMessage("Name must be unique").NotEmpty().Length(1, 100);
        RuleFor(x => x.IsActive).NotNull().Must(x => x == false || x == true);
        RuleFor(x => x.Discount).NotEmpty().LessThanOrEqualTo(100);
    }
    public bool IsNameUnique(MembershipDTO editedMembership, string newValue)
    {
        return _Membership.All(membership =>
          membership.Equals(editedMembership) || membership.Name != newValue);
    }
}
