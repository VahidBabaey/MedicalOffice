using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashPosValidator : AbstractValidator<CashPosDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IOfficeResolver _officeResolver;

    public AddCashPosValidator(ICashRepository cashRepository, IOfficeResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost).NotEmpty();
        Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
