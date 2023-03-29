using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashCheckValidator : AbstractValidator<CashCheckDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IRouteResolver _officeResolver;
    public AddCashCheckValidator(ICashRepository cashRepository, IRouteResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");

        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage(" ورود شماره حساب ضروری است")
            .Length(1, 13)
            .WithMessage("شماره حساب باید بین 1 تا 13 کاراکتر باشد");

    }
}
