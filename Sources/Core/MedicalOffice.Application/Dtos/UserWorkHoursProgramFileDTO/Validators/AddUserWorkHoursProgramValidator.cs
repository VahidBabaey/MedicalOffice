﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO.Validators
{
    public class AddUserWorkHoursProgramValidator : AbstractValidator<UserWorkHoursProgramDTO>
    {
        public AddUserWorkHoursProgramValidator()
        {
        }
    }
}
