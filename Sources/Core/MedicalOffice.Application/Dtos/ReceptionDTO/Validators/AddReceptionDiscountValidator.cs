using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class AddReceptionDiscountValidator : AbstractValidator<ReceptionDiscountDTO>
{
    private readonly IMembershipRepository _memberRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddReceptionDiscountValidator(IMembershipRepository memberRepository, IQueryStringResolver officeResolver)
    {
        _memberRepository = memberRepository;
        _officeResolver = officeResolver;

        Include(new MembershipIdValidator(_memberRepository, _officeResolver));
    }
}
