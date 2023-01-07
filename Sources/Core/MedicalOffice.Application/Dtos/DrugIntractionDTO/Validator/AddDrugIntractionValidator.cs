using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugIntractionDTO.Validator
{
    public class AddDrugIntractionValidator : AbstractValidator<DrugIntractionDTO>
    {
        public AddDrugIntractionValidator()
        {
            RuleFor(x => x.Effects).NotEmpty();
        }
    }
}
