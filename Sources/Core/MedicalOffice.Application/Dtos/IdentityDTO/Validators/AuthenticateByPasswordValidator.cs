using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class AuthenticateByPasswordValidator : AbstractValidator<AuthenticateByPasswordDTO>
    {
        public AuthenticateByPasswordValidator()
        {
        }
    }
}
