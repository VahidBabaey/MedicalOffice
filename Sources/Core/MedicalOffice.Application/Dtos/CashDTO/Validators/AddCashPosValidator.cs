﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashPosValidator : AbstractValidator<CashPosDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IContextResolver _officeResolver;

    public AddCashPosValidator(ICashRepository cashRepository, IContextResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
        //Include(new CashIdValidator(_cashRepository, _officeResolver));
    }
}
