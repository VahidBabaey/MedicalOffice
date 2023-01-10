using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class AddReceptionDetailValidator : AbstractValidator<ReceptionDetailDTO>
{
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IOfficeResolver _officeResolver;
    private readonly IServiceRepository _serviceRepository;
    public AddReceptionDetailValidator(IServiceRepository serviceRepository, IOfficeResolver officeResolver, IInsuranceRepository insuranceRepository)
    {
        _serviceRepository = serviceRepository;
        _officeResolver = officeResolver;
        _insuranceRepository = insuranceRepository;

        RuleFor(x => x.ServiceCount).NotEmpty();
        Include(new InsuranceIdValidator(_insuranceRepository, _officeResolver));
        Include(new ServiceIdValidator(_serviceRepository, _officeResolver));
    }
}
