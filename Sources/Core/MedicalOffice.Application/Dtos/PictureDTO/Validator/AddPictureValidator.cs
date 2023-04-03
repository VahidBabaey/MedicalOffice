using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.PictureDTO.Validator
{
    public class AddPictureValidator : AbstractValidator<PictureUploadDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContextResolver _officeResolver;
        public AddPictureValidator(IPatientRepository patientRepository, IContextResolver officeResolver)
        {
            _patientRepository = patientRepository;
            _officeResolver = officeResolver;
            Include(new PatientIdValidator(_patientRepository, _officeResolver));
        }
    }
}
