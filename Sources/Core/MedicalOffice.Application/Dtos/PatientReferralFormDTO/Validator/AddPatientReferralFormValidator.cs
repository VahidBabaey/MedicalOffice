using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator
{
    public class AddPatientReferralFormValidator : AbstractValidator<PatientReferralFormDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;
        public AddPatientReferralFormValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.ReferralReason).NotEmpty().Length(1, 200);
            RuleFor(x => x.DateSolar).NotEmpty().Length(1, 100);
            RuleFor(x => x.DateAD).NotEmpty();
            Include(new IPatientIdValidator(_patientRepository, _officeResolver));
        }
    }
}
