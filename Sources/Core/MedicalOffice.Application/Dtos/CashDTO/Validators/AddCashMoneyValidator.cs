using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashMoneyValidator : AbstractValidator<CashMoneyDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IRouteResolver _officeResolver;
    public AddCashMoneyValidator(ICashRepository cashRepository, IRouteResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
    }
}
