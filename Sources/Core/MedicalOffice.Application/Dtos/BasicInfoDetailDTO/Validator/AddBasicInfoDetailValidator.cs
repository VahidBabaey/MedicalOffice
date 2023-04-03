using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator
{
    public class AddBasicInfoDetailValidator : AbstractValidator<BasicInfoDetailDTO>
    {
        private readonly IBasicInfoDetailRepository _repositoryBasicInfoDetail;
        private readonly IContextResolver _officeResolver;
        public AddBasicInfoDetailValidator(IBasicInfoDetailRepository repositoryBasicInfoDetail, IContextResolver officeResolver)
        {
            _repositoryBasicInfoDetail = repositoryBasicInfoDetail;
            _officeResolver = officeResolver;
            RuleFor(x => x.InfoDetailName)
                .NotEmpty()
                .WithMessage("نام جزییات اطلاعات پایه موضوعی ضروری است.")
                .Length(1, 50)
                .WithMessage("طول جزییات اطلاعات پایه باید بین 1 تا 50 کاراکتر باشد.");

            Include(new BasicInfoDetailIdValidator(_repositoryBasicInfoDetail, _officeResolver));
        }
    }
}
