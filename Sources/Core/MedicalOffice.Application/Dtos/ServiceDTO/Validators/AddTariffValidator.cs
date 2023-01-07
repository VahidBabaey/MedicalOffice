using FluentValidation;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using System.Text.RegularExpressions;

namespace MedicalOffice.Application.Dtos.CashDTO.Validators;

public class AddTariffValidator : AbstractValidator<ServiceTariffDTO>
{
    public AddTariffValidator()
    {

        RuleFor(x => x.TariffValue).NotEmpty();
        RuleFor(x => x.InternalTariffValue).NotEmpty();
        RuleFor(x => x.Difference).NotEmpty();
       
    }
}
