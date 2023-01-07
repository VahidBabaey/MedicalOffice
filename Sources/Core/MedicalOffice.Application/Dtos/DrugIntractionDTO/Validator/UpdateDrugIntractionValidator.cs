using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugIntractionDTO.Validator
{
    public class UpdateDrugIntractionValidator : AbstractValidator<UpdateDrugIntractionDTO>
    {
        public UpdateDrugIntractionValidator()
        {
            RuleFor(x => x.Group1).NotEmpty();
            RuleFor(x => x.Group2).NotEmpty();
            RuleFor(x => x.Effects).NotEmpty();
            RuleFor(x => x.Control).NotEmpty();
            RuleFor(x => x.Method).NotEmpty();
        }
    }
}
