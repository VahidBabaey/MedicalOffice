using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormIllnessDTO.Validators
{
    public class UpdateFormIllnessValidator : AbstractValidator<UpdateFormIllnessDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;
        public UpdateFormIllnessValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
