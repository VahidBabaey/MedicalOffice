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
    public class TelePhoneNumberValidator : AbstractValidator<ITelePhoneNumberDTO>
    {
        public TelePhoneNumberValidator()
        {
            RuleFor(x => x.TelePhoneNumber)
           .NotEmpty()
           //.WithMessage(ValidationMessage.Required.For("TelePhoneNumber"))
           .WithMessage("ورود شماره تلفن ضروری است")
           .Must(p => IsValidTelePhoneNumber(p))
           //.WithMessage(ValidationMessage.NotValid.For("TelePhoneNumber"))
           .WithMessage("شماره تلفن وارد شده موجود نیست");
        }
        private static bool IsValidTelePhoneNumber(string telePhoneNumber)
        {
            Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

            return regex.IsMatch(telePhoneNumber);
        }
    }
}
