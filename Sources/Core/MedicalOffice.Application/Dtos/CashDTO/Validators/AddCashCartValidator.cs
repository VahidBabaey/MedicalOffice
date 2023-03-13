using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashCartValidator : AbstractValidator<CashCartDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddCashCartValidator(ICashRepository cashRepository, IQueryStringResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");

        RuleFor(x => x.CartNumber)
            .NotEmpty()
            .WithMessage("شماره کارت ضروری است")
            .Length(1, 16)
            .WithMessage("طول جزییات اطلاعات پایه باید بین 1 تا 16 کاراکتر باشد");

        Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
