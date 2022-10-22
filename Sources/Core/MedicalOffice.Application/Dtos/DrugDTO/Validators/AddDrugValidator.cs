using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.DrugDTO.Validators
{
    public class AddDrugValidator : AbstractValidator<DrugDTO>
    {
        public AddDrugValidator()
        {

        }
    }
}
