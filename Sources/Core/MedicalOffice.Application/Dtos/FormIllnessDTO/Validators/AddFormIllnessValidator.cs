using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormIllnessDTO.Validators
{
    public class AddFormIllnessValidator : AbstractValidator<AddFormIllnessDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IOfficeResolver _officeResolver;
        public AddFormIllnessValidator(IPatientRepository patientRepository, IOfficeResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
