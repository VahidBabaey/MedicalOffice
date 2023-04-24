using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class PatientIdValidator : AbstractValidator<PatientIdDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;

        public PatientIdValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            Include(new IPatientIdValidator(_patientRepository, _officeResolver));
        }
    }
}
