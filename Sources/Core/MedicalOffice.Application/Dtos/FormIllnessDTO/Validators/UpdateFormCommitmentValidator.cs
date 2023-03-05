﻿using FluentValidation;
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
    public class UpdateFormIllnessValidator : AbstractValidator<UpdateFormIllnessDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IQueryStringResolver _officeResolver;
        public UpdateFormIllnessValidator(IPatientRepository patientRepository, IQueryStringResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
