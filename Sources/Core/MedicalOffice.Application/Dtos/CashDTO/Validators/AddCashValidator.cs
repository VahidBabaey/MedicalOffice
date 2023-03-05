using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashValidator : AbstractValidator<CashesDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddCashValidator(ICashRepository cashRepository, IQueryStringResolver officeResolver)
    {
        _officeResolver = officeResolver;
        _cashRepository = cashRepository;

        RuleFor(x => x.Recieved).NotEmpty();
        Include(new CashReceptionIdValidator(_cashRepository, _officeResolver));
    }
}
