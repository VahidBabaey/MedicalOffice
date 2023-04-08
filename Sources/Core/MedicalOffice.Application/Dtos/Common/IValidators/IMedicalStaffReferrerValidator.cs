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
    public class IMedicalStaffReferrerValidator : AbstractValidator<IReferrerMedicalStaffIdDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IContextResolver _officeResolver;
        public IMedicalStaffReferrerValidator(IMedicalStaffRepository medicalStaffRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _medicalStaffRepository = medicalStaffRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ReferrerMedicalStaffId)
               
               .MustAsync(async (ReferrerMedicalStaffId, token) =>
               {
                   if (ReferrerMedicalStaffId == null)
                   {
                       return true;
                   }
                   else
                   {
                       return await _medicalStaffRepository.CheckMedicalStaffReferrerExist(ReferrerMedicalStaffId, officeId);
                   }
               })
               .WithMessage("{PropertyName} isn't exist");
        }
    }
}
