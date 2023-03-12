using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashCheckValidator : AbstractValidator<CashCheckDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddCashCheckValidator(ICashRepository cashRepository, IQueryStringResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.AccountNumber).NotEmpty().Length(1, 13);
        Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
