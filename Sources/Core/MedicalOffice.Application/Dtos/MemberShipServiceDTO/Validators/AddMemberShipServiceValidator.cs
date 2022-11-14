using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators
{
    internal class AddMemberShipServiceValidator : AbstractValidator<MemberShipServiceDTO>
    {
        public AddMemberShipServiceValidator()
        {
        }
    }
}
