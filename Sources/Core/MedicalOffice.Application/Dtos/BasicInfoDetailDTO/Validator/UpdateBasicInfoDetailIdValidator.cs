﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Domain.Entities;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator
{
    public class UpdateBasicInfoDetailIdValidator : AbstractValidator<IBasicInfoDetailIdDTO>
    {
        private readonly IBasicInfoDetailRepository _repositoryBasicInfoDetail;
        private readonly IOfficeResolver _officeResolver;
        public UpdateBasicInfoDetailIdValidator(IBasicInfoDetailRepository repositoryBasicInfoDetail, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _repositoryBasicInfoDetail = repositoryBasicInfoDetail;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.BasicInfoId)
                .NotEmpty()
                .MustAsync(async (basicInfoId, token) =>
                {
                    var leaveTypeExists = await _repositoryBasicInfoDetail.CheckExistBasicInfoId(officeId, basicInfoId);
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

