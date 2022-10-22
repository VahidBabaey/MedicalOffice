using FluentValidation;

namespace MedicalOffice.Application.Dtos.ServiceDTO.Validators;

public class AddServiceValidator : AbstractValidator<ServiceDTO>
{
    public AddServiceValidator()
    {
    }
}
