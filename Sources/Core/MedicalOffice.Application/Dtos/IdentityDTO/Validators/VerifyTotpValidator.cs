using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class VerifyTotpValidator : AbstractValidator<VerifyTotpDTO>
    {
        public VerifyTotpValidator()
        {
            Include(new PhoneNumberValidator());
        }
    }
}
