using FluentValidation;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceDTO>
{
    public UpdateServiceValidator()
    {

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).Length(1, 200);
        RuleFor(x => x.GenericCode).NotEmpty();

    }
}
