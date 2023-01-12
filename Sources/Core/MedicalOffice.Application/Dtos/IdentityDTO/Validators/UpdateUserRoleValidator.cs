using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleDTO>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserRoleValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            Include(new UserByPhoneNumberValidator(_userManager));
        }
    }
}
