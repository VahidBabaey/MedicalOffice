using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class IPhoneNumberListValidator : AbstractValidator<IPhoneNumberListDTO>
    {
        public IPhoneNumberListValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("The PhoneNumber is required")
                .Must(x => IsValidPhoneNumberList(x)).WithMessage("Phone number is not valid");
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
