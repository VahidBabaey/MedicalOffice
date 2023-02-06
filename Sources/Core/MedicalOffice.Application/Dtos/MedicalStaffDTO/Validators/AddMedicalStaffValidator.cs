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
            Include(new NationalIdValidator());
            Include(new PhoneNumberValidator());
        }
    }
}
