using FluentValidation;

namespace MedicalOffice.Application.Dtos.Service.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    public AddServiceValidator()
    {
    }
}
