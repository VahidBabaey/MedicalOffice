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
        private readonly IQueryStringResolver _officeResolver;
        private readonly IOfficeRepository _officeRepository;

        public UpdateOfficeValidator(
            IQueryStringResolver officeResolver,
            IOfficeRepository officeRepository)
        {
            _officeResolver = officeResolver;
            _officeRepository = officeRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            Include(new TelePhoneNumberValidator());

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("minimum length of {PropertyName} is 3");
        }
    }
}
