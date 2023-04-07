using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class ITelePhoneNumberValidator : AbstractValidator<ITelePhoneNumberDTO>
    {
        public ITelePhoneNumberValidator()
        {
            RuleFor(x => x.TelePhoneNumber)
           .Must(p => IsValidTelePhoneNumber(p))
           //.WithMessage(ValidationMessage.NotValid.For("TelePhoneNumber"))
           .WithMessage("شماره تلفن وارد شده نامعتبر است.");
        }
        private static bool IsValidTelePhoneNumber(string telePhoneNumber)
        {
            if (telePhoneNumber != string.Empty)
            {
                Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

                return regex.IsMatch(telePhoneNumber);
            }
            return true;
        }
    }
}
