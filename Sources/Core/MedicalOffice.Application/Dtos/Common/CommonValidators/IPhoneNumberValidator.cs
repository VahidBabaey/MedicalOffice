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
    public class IPhoneNumberValidator : AbstractValidator<IPhoneNumberDTO>
    {
        private static readonly int MaximumLength = 11;
        public IPhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(ValidationMessage.Required.For("PhoneNumber"))
                .MaximumLength(11).WithMessage(ValidationMessage.MaximumLength.For("PhoneNumber", MaximumLength))
                .Must(x => IsValidPhoneNumber(x)).WithMessage(ValidationMessage.NotValid.For("PhoneNumber"));
        }
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

            return regex.IsMatch(phoneNumber);
        }
    }
}
