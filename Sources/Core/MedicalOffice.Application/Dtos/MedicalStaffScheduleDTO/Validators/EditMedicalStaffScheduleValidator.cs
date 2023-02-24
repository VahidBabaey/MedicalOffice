using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators
{
    public class EditMedicalStaffScheduleValidator : AbstractValidator<MedicalStaffScheduleDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IOfficeResolver _officeResolver;

        public EditMedicalStaffScheduleValidator(
            IMedicalStaffRepository medicalStaffRepository,
            IOfficeResolver officeResolver)
        {
            _medicalStaffRepository = medicalStaffRepository;
            _officeResolver = officeResolver;

            Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));

            RuleForEach(x => x.MedicalStaffSchedule)
                .SetValidator(new MedicalStaffDayScheduleValidator());
        }
    }
}
