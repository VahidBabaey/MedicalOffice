using FluentValidation;
using MedicalOffice.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class ITelePhoneNumberValidator : AbstractValidator<ITelePhoneNumberDTO>
    {
        public ITelePhoneNumberValidator()
        {
            RuleFor(p => p.TelePhoneNumber)
           .NotEmpty().WithMessage(ValidationMessage.Required.For<ITelePhoneNumberDTO>(p => p.TelePhoneNumber))
           .Must(p => IsValidTelePhoneNumber(p)).WithMessage(ValidationMessage.NotValid.For<ITelePhoneNumberDTO>(p => p.TelePhoneNumber));
        }
        bool IsValidTelePhoneNumber(string telePhoneNumber)
        {
            Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

            return regex.IsMatch(telePhoneNumber);
        }
    }
}
