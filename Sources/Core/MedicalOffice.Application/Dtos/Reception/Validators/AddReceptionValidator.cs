using FluentValidation;
using MedicalOffice.Application.Dtos.Reception;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddReceptionValidator : AbstractValidator<ReceptionDTO>
{
    public AddReceptionValidator()
    {
    }
}
