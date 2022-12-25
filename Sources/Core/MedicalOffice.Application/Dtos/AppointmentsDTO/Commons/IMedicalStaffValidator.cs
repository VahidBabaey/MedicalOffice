using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Commons
{
    public class IMedicalStaffValidator: AbstractValidator<IMedicalStaffDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;

        public IMedicalStaffValidator()
        {
            RuleFor(x => x.MedicalStaffId)
               .NotEmpty()
               .WithMessage("{PropertyName} is required")
               .Must(x => isStaffExist(x))
               .WithMessage("{PropertyName} isn't exist");
        }
        private bool isStaffExist(Guid staffId)
        {
            var existingStaff = _medicalStaffRepository.GetById(staffId);

            if (existingStaff != null)
            {
                return true;
            }

            return false;
        }
    }
}
