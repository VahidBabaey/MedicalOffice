using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PatientIllnessFormDTO.Validator
{
    public class AddPatientIllnessFormValidator : AbstractValidator<PatientIllnessFormDTO>
    {
        public AddPatientIllnessFormValidator()
        {
            RuleFor(x => x.IllnessReason).NotEmpty().Length(1, 200);
            RuleFor(x => x.DateSolar).NotEmpty().Length(1, 50);
            RuleFor(x => x.DateAD).NotEmpty();
            RuleFor(x => x.Duration).NotEmpty().Length(1, 100);
            RuleFor(x => x.RestPlace).NotEmpty().Length(1, 100);
        }
    }
}
