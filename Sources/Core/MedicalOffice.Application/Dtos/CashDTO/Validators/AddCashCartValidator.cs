using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashCartValidator : AbstractValidator<CashCartDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IOfficeResolver _officeResolver;
    public AddCashCartValidator(ICashRepository cashRepository, IOfficeResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.CartNumber).NotEmpty().Length(1, 16);
        RuleFor(x => x.Date).NotEmpty();
        Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
