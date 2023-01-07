using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugDTO.Validators
{
    public class UpdateDrugValidator : AbstractValidator<UpdateDrugDTO>
    {
        public UpdateDrugValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 100);
            RuleFor(x => x.GenericCode).NotEmpty();
            RuleFor(x => x.BrandName).NotEmpty().Length(1, 100);
            RuleFor(x => x.Consumption).NotEmpty().Length(1, 100);
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.IsShow).NotEmpty();
        }
    }
}
