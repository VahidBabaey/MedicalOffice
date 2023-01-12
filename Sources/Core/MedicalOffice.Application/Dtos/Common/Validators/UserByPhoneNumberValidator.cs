using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class UserByPhoneNumberValidator : AbstractValidator<IPhoneNumberDTO>
    {
        private readonly UserManager<User> _userManager;

        public UserByPhoneNumberValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MustAsync(async (phoneNumber, token) =>
            {
                return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
            })
            .WithMessage("The {PropertyName} is't exist.");
        }
    }
}
