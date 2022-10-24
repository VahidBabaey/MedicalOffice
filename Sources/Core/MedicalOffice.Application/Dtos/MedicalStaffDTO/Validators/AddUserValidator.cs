using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.UserDTO.Validators
{
    public class AddUserValidator : AbstractValidator<MedicalStaffDTO>
    {
        public AddUserValidator()
        {
        }
    }
}
