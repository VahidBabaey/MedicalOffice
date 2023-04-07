using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class AddReceptionDiscountValidator : AbstractValidator<ReceptionDiscountDTO>
{
    private readonly IMembershipRepository _memberRepository;
    private readonly IContextResolver _officeResolver;
    public AddReceptionDiscountValidator(IMembershipRepository memberRepository, IContextResolver officeResolver)
    {
        _memberRepository = memberRepository;
        _officeResolver = officeResolver;

        Include(new IMembershipIdValidator(_memberRepository, _officeResolver));
    }
}
