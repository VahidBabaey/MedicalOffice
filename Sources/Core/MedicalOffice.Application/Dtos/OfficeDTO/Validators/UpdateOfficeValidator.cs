using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators
{
    public class UpdateOfficeValidator : AbstractValidator<UpdateOfficeDTO>
    {
        private readonly IOfficeResolver _officeResolver;
        private readonly IOfficeRepository _officeRepository;

        public UpdateOfficeValidator(
            IOfficeResolver officeResolver,
            IOfficeRepository officeRepository)
        {
            _officeResolver = officeResolver;
            _officeRepository = officeRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            Include(new TelePhoneNumberValidator());
            Include(new TelePhoneNumberExistValidator(_officeRepository));

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("minimum length of {PropertyName} is 3");

            RuleFor(o => o.Address)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(10)
                .WithMessage("minimum length of {PropertyName} is 10");

            RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (Id, Token) =>
            {
                return await _officeRepository.IsOfficeExist(Id);
            });
        }
    }
}
