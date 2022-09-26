using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator
{
    public class AddPatientReferralFormValidator : AbstractValidator<PatientReferralFormDTO>
    {
        public AddPatientReferralFormValidator()
        {
        }
    }
}
