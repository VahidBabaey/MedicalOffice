using FluentValidation;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators
{
    public class UserOfficeValidator : AbstractValidator<UserOfficeDTO>
    {
        public UserOfficeValidator()
        {
            Include(new ITelePhoneNumberValidator());
        }
    }
}
