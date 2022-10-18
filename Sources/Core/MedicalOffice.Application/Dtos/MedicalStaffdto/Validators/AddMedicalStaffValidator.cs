using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Userdto.Validators
{
    public class AddUserValidator : AbstractValidator<UserDTO>
    {
        public AddUserValidator()
        {
        }
    }
}
