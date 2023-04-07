﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashMoneyValidator : AbstractValidator<CashMoneyDTO>
{
    private readonly ICashRepository _cashRepository;
    private readonly IContextResolver _officeResolver;
    public AddCashMoneyValidator(ICashRepository cashRepository, IContextResolver officeResolver)
    {
        _cashRepository = cashRepository;
        _officeResolver = officeResolver;
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage("ورود مبلغ ضروری است");
    }
}
