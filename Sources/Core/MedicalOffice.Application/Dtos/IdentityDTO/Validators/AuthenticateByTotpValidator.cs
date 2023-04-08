
using FluentValidation;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Common.IValidators;
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
    public class AuthenticateByTotpValidator : AbstractValidator<AuthenticateByTotpDTO>
    {
        private readonly UserManager<User> _userManager;

        public AuthenticateByTotpValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            Include(new IUserByPhoneNumberValidator(_userManager));
        }
    }
}
