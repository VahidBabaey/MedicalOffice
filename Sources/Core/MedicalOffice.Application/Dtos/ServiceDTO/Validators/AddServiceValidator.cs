﻿using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Commons;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IOfficeResolver _officeResolver;
    public AddServiceValidator(ISectionRepository sectionRepository, IOfficeResolver officeResolver)
    {
        _officeResolver = officeResolver;
        _sectionRepository = sectionRepository;
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.GenericCode).NotEmpty();
        Include(new SectionIdValidator(_sectionRepository, _officeResolver));
    }
}
