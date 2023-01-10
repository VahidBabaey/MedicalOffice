using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
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
        public UpdateUserRoleValidator()
        {
            Include(new Common.CommonValidators.PhoneNumberValidator());
        }
    }
}
