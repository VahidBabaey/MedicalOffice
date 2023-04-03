﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators
{
    public class AddMedicalStaffScheduleValidator : AbstractValidator<MedicalStaffScheduleDTO>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IContextResolver _officeResolver;

        public AddMedicalStaffScheduleValidator(
            IMedicalStaffRepository medicalStaffRepository,
            IContextResolver officeResolver)
        {
            _medicalStaffRepository = medicalStaffRepository;
            _officeResolver = officeResolver;

            Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));
            RuleForEach(x => x.MedicalStaffSchedule)
                .SetValidator(new MedicalStaffDayScheduleValidator());
        }
    }
}

