using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class MedicalStaffReferrerValidator : AbstractValidator<IReferrerMedicalStaffIdDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IOfficeResolver _officeResolver;
        public MedicalStaffReferrerValidator(IMedicalStaffRepository medicalStaffRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _medicalStaffRepository = medicalStaffRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ReferrerMedicalStaffId)
               .NotEmpty()
               .MustAsync(async (ReferrerMedicalStaffId, token) =>
               {
                   return await _medicalStaffRepository.CheckMedicalStaffReferrerExist(ReferrerMedicalStaffId, officeId);
               })
               .WithMessage("{PropertyName} isn't exist");
        }
    }
}
