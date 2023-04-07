using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IUserByPhoneNumberValidator : AbstractValidator<IPhoneNumberDTO>
    {
        private readonly UserManager<User> _userManager;

        public IUserByPhoneNumberValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("ورود شماره تلفن ضروری است")
            .MustAsync(async (phoneNumber, token) =>
            {
                return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
            })
            //.WithMessage("The {PropertyName} is't exist.");
            .WithMessage("شماره تلفن موجود نیست");
        }
    }
}
