using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;

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
