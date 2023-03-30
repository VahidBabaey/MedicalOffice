using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PatientIllnessFormDTO.Validator
{
    public class AddPatientIllnessFormValidator : AbstractValidator<PatientIllnessFormDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;
        public AddPatientIllnessFormValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.IllnessReason).NotEmpty().Length(1, 200);
            RuleFor(x => x.DateSolar).NotEmpty().Length(1, 50);
            RuleFor(x => x.DateAD).NotEmpty();
            RuleFor(x => x.Duration).NotEmpty().Length(1, 100);
            RuleFor(x => x.RestPlace).NotEmpty().Length(1, 100);
            Include(new PatientIdValidator(_patientRepository, _officeResolver));
        }
    }
}
