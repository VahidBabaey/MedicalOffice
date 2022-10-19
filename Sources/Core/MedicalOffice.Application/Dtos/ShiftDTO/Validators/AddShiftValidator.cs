using FluentValidation;
using MedicalOffice.Application.Dtos.ShiftDTO;

namespace MedicalOffice.Application.Dtos.ShiftDTO.Validators;

public class AddShiftValidator : AbstractValidator<ShiftDTO>
{
    public AddShiftValidator()
    {
    }
}
