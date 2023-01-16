﻿using FluentValidation;
using MedicalOffice.Application.Dtos.ShiftDTO;

namespace MedicalOffice.Application.Dtos.ShiftDTO.Validators;

public class AddShiftValidator : AbstractValidator<ShiftDTO>
{
    public AddShiftValidator()
    {

        RuleFor(x => x.Name).NotEmpty().Length(1, 100);
        RuleFor(x => x.StartTime).NotEmpty().Length(1, 10);
        RuleFor(x => x.EndTime).NotEmpty().Length(1, 10);
        RuleFor(x => x.HolidayShift).NotEmpty();

    }
}
