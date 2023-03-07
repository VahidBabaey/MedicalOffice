using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators
{
    public class AddMedicalStaffValidator : AbstractValidator<MedicalStaffDTO>
    {
        public AddMedicalStaffValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            //RuleFor(x => x.IsReferrer).NotNull().Must(x => x == false || x == true);
            Include(new NationalIdValidator());
            Include(new PhoneNumberValidator());
        }
    }
}
