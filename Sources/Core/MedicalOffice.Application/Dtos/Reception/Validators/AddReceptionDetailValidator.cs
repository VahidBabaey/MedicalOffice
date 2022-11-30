using FluentValidation;
using MedicalOffice.Application.Dtos.Reception;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddReceptionDetailValidator : AbstractValidator<ReceptionDetailDTO>
{
    public AddReceptionDetailValidator()
    {
    }
}
