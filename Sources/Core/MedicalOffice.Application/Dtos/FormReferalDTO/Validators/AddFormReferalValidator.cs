using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.FormReferalDTO.Validators
{
    public class AddFormReferalValidator : AbstractValidator<AddFormReferalDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IQueryStringResolver _officeResolver;
        public AddFormReferalValidator(IPatientRepository patientRepository, IQueryStringResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
