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
    public class PhoneNumberValidator : AbstractValidator<IPhoneNumberDTO>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                //.WithMessage(ValidationMessage.Required.For("PhoneNumber"))
                .WithMessage("ورود شماره تلفن ضروری است")
                .Must(x => IsValidPhoneNumber(x))
                //.WithMessage(ValidationMessage.NotValid.For("PhoneNumber"));
                .WithMessage("شماره تلفن معتبر نیست.");
        }
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(0|0098|\+98)9(0[1-5]|[1 3]\d|2[0-2]|98)\d{7}$");

            return regex.IsMatch(phoneNumber);
        }

        bool IsValidPhoneNumberList(string[] phoneNumbers)
        {

            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

            foreach (string phoneNumber in phoneNumbers)
            {
                if (!regex.IsMatch(phoneNumber))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
