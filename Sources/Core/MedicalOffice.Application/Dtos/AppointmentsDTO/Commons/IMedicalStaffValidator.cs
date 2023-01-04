using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Commons
{
    public class IMedicalStaffValidator : AbstractValidator<IMedicalStaffDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        public IMedicalStaffValidator(IMedicalStaffRepository medicalStaffRepository)
        {
            _medicalStaffRepository = medicalStaffRepository;

            RuleFor(x => x.MedicalStaffId)
               .NotEmpty()
               .MustAsync(async (id, token) =>
               {
                   var isMedicalStaffExist = await _medicalStaffRepository.GetById(id);
                   if (isMedicalStaffExist != null)
                   {
                       return true;
                   }
                   return false;
               })
               .WithMessage("{PropertyName} isn't exist");
        }
    }
}
