using FluentValidation;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    public AddServiceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 200);
        RuleFor(x => x.GenericCode).NotEmpty();
    }
}
