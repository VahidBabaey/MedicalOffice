using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class PatientIdValidator : AbstractValidator<IPatientIdDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IOfficeResolver _officeResolver;

        public PatientIdValidator(IPatientRepository patientRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _patientRepository = patientRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.PatientId)
                .NotEmpty()
                .MustAsync(async (patientId, token) =>
                {
                    var leaveTypeExists = await _patientRepository.CheckExistPatientId(officeId, patientId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
