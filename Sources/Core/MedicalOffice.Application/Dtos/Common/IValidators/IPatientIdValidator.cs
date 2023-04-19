using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IPatientIdValidator : AbstractValidator<IPatientIdDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;

        public IPatientIdValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _patientRepository = patientRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.PatientId)
                .NotEmpty()
                .MustAsync(async (patientId, token) =>
                {
                    return await _patientRepository.CheckExistPatientId(officeId, patientId);
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
