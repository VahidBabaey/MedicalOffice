using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator
{
    public class AddBasicInfoDetailValidator : AbstractValidator<BasicInfoDetailDTO>
    {
        public AddBasicInfoDetailValidator()
        {
            RuleFor(x => x.InfoDetailName).NotEmpty().Length(1, 50);
        }
    }
}
