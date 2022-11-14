using FluentValidation;
using MedicalOffice.Application.Dtos.Reception;

namespace MedicalOffice.Application.Dtos.SectionDTO.Validators;

public class AddReceptionDiscountValidator : AbstractValidator<ReceptionDiscountDTO>
{
    public AddReceptionDiscountValidator()
    {
    }
}
