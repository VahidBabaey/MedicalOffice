using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.CommonValidators;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators;

public class AddOfficeDtoValidator : AbstractValidator<AddOfficeDto>
{
    private readonly IOfficeRepository _officeRepository;
    public AddOfficeDtoValidator(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;

        Include(new TelePhoneNumberValidator());
        Include(new TelePhoneNumberExistValidator(_officeRepository));

        RuleFor(o => o.Name)
            .NotEmpty()
            //.WithMessage("{PropertyName} is required")
            .WithMessage("ورود نام ضروری است")
            .MinimumLength(3)
            //.WithMessage("minimum length of {PropertyName} is 3");
            .WithMessage("نام نباید کمتر از 3 کاراکتر باشد");

        RuleFor(o => o.Address)
            .NotEmpty()
            //.WithMessage("{PropertyName} is required")
            .WithMessage("ورود آدرس ضروری است")
            .MinimumLength(10)
            .WithMessage("آدرس نباید کمتر از 10 کاراکتر باشد");
    }
}
