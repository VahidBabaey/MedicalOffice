using FluentValidation;
using MedicalOffice.Application.Dtos.Shift;

namespace MedicalOffice.Application.Dtos.Service.Validators;

public class AddShiftValidator : AbstractValidator<ShiftDTO>
{
    public AddShiftValidator()
    {
    }
}
