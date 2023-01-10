﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class InsuranceIdValidator : AbstractValidator<IInsuranceIdDTO>
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IOfficeResolver _officeResolver;
        public InsuranceIdValidator(IInsuranceRepository insuranceRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _insuranceRepository = insuranceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.InsuranceId)
                .NotEmpty()
                .MustAsync(async (insuranceId, token) =>
                    {
                        return await _insuranceRepository.CheckExistInsuranceId(insuranceId, officeId);

                    })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
