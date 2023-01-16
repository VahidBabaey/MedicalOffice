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
    public class UpdateBasicInfoDetailValidator : AbstractValidator<UpdateBasicInfoDetailDTO>
    {
        private readonly IBasicInfoDetailRepository _repositoryBasicInfoDetail;
        private readonly IOfficeResolver _officeResolver;
        public UpdateBasicInfoDetailValidator(IBasicInfoDetailRepository repositoryBasicInfoDetail, IOfficeResolver officeResolver)
        {
            _repositoryBasicInfoDetail = repositoryBasicInfoDetail;
            _officeResolver = officeResolver;
            RuleFor(x => x.InfoDetailName).NotEmpty().Length(1, 50);
            Include(new UpdateBasicInfoDetailIdValidator(_repositoryBasicInfoDetail, _officeResolver));
        }
    }
}
