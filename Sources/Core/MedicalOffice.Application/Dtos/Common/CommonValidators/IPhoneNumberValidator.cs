using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class IPhoneNumberValidator: AbstractValidator<IPhoneNumberDTO>
    {
        public IPhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("The PhoneNumber is required")
                .MaximumLength(11).WithMessage("Maximum length of phone number is 11")
                .Must(x => IsValidPhoneNumber(x)).WithMessage("Phone number is not valid");
        }
        bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

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
