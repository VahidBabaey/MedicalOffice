﻿using FluentValidation;


namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddCashPosValidator : AbstractValidator<CashPosDTO>
{
    public AddCashPosValidator()
    {
        RuleFor(x => x.Cost).NotEmpty();
    }
}
