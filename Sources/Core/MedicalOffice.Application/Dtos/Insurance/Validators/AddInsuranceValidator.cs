using FluentValidation;

namespace MedicalOffice.Application.Dtos.Insurance.Validators;

public class AddInsuranceValidator : AbstractValidator<InsuranceDTO>
{
    public AddInsuranceValidator()
    {
    }
}
