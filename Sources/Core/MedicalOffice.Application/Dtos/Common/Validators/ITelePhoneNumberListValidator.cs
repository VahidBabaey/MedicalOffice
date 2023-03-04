using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class ITelePhoneNumberListValidator : AbstractValidator<ITelePhoneNumberListDTO>
    {
        public ITelePhoneNumberListValidator()
        {
            RuleFor(p => p.TelePhoneNumber)
           .Must(p => IsValidTelePhoneNumberList(p)).WithMessage("{PropertyName} is not valid");
        }
        bool IsValidTelePhoneNumberList(string[] telePhoneNumbers)
        {
            Regex regex = new Regex(@"^0[0-9]{2,}[0-9]{7,}$");

            foreach (string telNumber in telePhoneNumbers)
            {
                if (!regex.IsMatch(telNumber))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
