using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashMoneyValidator : AbstractValidator<CashMoneyDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddCashMoneyValidator(ICashRepository cashRepository, IQueryStringResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost).NotEmpty();
        Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
