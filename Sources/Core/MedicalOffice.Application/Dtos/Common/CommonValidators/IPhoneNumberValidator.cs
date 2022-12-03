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
    public class IPhoneNumberValidator: AbstractValidator<IPhoneNumberDTO>
    {
        public IPhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty)
                .MaximumLength(11).WithMessage($"{ValidationErrorMessages.MaximumLength} 11")
                .Must(x => IsValidPhoneNumber(x)).WithMessage(ValidationErrorMessages.NotValid);
        }
        bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

            return regex.IsMatch(phoneNumber);
        }
    }
}
