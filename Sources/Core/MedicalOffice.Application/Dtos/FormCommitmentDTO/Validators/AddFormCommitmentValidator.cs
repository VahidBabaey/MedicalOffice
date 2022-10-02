using FluentValidation;
using MedicalOffice.Application.Dtos.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormCommitmentDTO.Validators
{
    public class AddFormCommitmentValidator : AbstractValidator<FormCommitmentDTO>
    {
        public AddFormCommitmentValidator()
        {
        }
    }
}
