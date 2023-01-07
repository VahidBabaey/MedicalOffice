using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator
{
    public class IBasicInfoDetailValidator : AbstractValidator<BasicInfoDetailDTO>
    {
        private readonly IBasicInfoDetailRepository _repository;
        public IBasicInfoDetailValidator(IBasicInfoDetailRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.InfoDetailName).NotEmpty().Length(1, 50);
            //RuleFor(x => x.BasicInfoId).NotEmpty().MustAsync(async (officeId, basicInfoId, token) =>
            //{
            //    var basicInfoIdExist = await _repository.CheckExistBasicInfoId(officeId, basicInfoId);
            //    if (basicInfoIdExist)
            //    {
            //        return true;
            //    }
            //    return false;
            //})
            //    .WithMessage("{PropertyName} is not exist");

        }
    }
}
