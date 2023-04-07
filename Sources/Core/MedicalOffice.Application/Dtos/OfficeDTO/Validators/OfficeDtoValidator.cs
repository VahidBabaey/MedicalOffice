using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators;

public class OfficeDTOValidator : AbstractValidator<OfficeDTO>
{
    private readonly IOfficeRepository _officeRepository;
    public OfficeDTOValidator(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;

        Include(new ITelePhoneNumberValidator());
        Include(new TelePhoneNumberExistValidator(_officeRepository));

        RuleFor(o => o.Name)
            .NotEmpty()
            //.WithMessage("{PropertyName} is required")
            .WithMessage("ورود نام ضروری است")
            .MinimumLength(3)
            //.WithMessage("minimum length of {PropertyName} is 3");
            .WithMessage("نام نباید کمتر از 3 کاراکتر باشد");
    }
}
