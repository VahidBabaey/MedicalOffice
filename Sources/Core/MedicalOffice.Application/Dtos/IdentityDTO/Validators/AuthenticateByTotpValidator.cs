
using FluentValidation;
using MedicalOffice.Application.Dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class AuthenticateByTotpValidator : AbstractValidator<AuthenticateByTotpDTO>
    {
        public AuthenticateByTotpValidator()
        {

        }
    }
}
