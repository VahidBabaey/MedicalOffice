using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Entities;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator
{
    public class BasicInfoDetailIdValidator : AbstractValidator<BasicInfoDetailDTO>
    {
        private readonly IBasicInfoDetailRepository _repositoryBasicInfoDetail;
        private readonly IRouteResolver _officeResolver;
        public BasicInfoDetailIdValidator(IBasicInfoDetailRepository repositoryBasicInfoDetail, IRouteResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _repositoryBasicInfoDetail = repositoryBasicInfoDetail;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.BasicInfoId)
                .NotEmpty()
                .WithMessage("شناسه اطلاعات پایه موضوعی ضروری است")
                .MustAsync(async (basicInfoId, token) =>
                {
                    var leaveTypeExists = await _repositoryBasicInfoDetail.CheckExistBasicInfoId(officeId, basicInfoId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                //.WithMessage("{PropertyName} isn't exist");
                .WithMessage("شناسه اطلاعات پایه موضوعی موجود نیست");
        }
    }
}

