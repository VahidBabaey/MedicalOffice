using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IMedicalStaffValidator : AbstractValidator<IMedicalStaffDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IContextResolver _officeResolver;
        public IMedicalStaffValidator(IMedicalStaffRepository medicalStaffRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _medicalStaffRepository = medicalStaffRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.MedicalStaffId)
               .NotEmpty()
               .MustAsync(async (medicalStaffId, token) =>
               {
                   return await _medicalStaffRepository.CheckMedicalStaffExist(medicalStaffId, officeId);
               })
               .WithMessage("{PropertyName} isn't exist");
        }
    }
}
