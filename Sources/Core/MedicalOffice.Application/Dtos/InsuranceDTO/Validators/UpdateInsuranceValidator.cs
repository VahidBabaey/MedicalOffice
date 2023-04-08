using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;

namespace MedicalOffice.Application.Dtos.InsuranceDTO.Validators;

public class UpdateInsuranceValidator : AbstractValidator<UpdateInsuranceDTO>
{
    private readonly IInsuranceRepository _insurancerepository;
    private readonly IContextResolver _officeResolver;

    public UpdateInsuranceValidator(IInsuranceRepository insurancerepository,IContextResolver contextResolver)
    {
        _insurancerepository = insurancerepository;
        _officeResolver = contextResolver;

        var officeId = _officeResolver.GetOfficeId().Result;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام بیمه ضروری است")
            .Length(1, 100)
            .WithMessage("نام بیمه باید بین 1 تا 100 کاراکتر داشته باشد");
        RuleFor(x => x.InsurancePercent)
            .GreaterThanOrEqualTo(0)    
            .LessThanOrEqualTo(100)
            .WithMessage("درصد سازمان عددی در بازه 0 تا 100 است.");

        RuleFor(x => x.Id)
            .NotEmpty()
        .MustAsync(async (ins, token) =>
        {
            return await _insurancerepository.CheckExistInsuranceId(officeId, ins);
        });
    }
}
