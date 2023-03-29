using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashValidator : AbstractValidator<CashesDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IRouteResolver _officeResolver;
    public AddCashValidator(ICashRepository cashRepository, IRouteResolver officeResolver)
    {
        _officeResolver = officeResolver;
        _cashRepository = cashRepository;

        RuleFor(x => x.Recieved)
            .NotEmpty()
            .WithMessage("ورود مبلغ دریافتی ضروری است");

        Include(new CashReceptionIdValidator(_cashRepository, _officeResolver));
    }
}
