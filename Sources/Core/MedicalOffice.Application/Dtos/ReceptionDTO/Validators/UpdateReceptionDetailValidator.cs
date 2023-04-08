using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class UpdateReceptionDetailValidator : AbstractValidator<UpdateReceptionDetailDTO>
{
    private readonly IReceptionRepository _receptionRepository;
    private readonly IInsuranceRepository _insuranceRepository;
    private readonly IContextResolver _QueryStringResolver;
    private readonly IServiceRepository _serviceRepository;
    public UpdateReceptionDetailValidator(IReceptionRepository receptionRepository, IServiceRepository serviceRepository, IContextResolver QueryStringResolver, IInsuranceRepository insuranceRepository)
    {
        _receptionRepository = receptionRepository;
        _serviceRepository = serviceRepository;
        _QueryStringResolver = QueryStringResolver;
        _insuranceRepository = insuranceRepository;

        RuleFor(x => x.ServiceCount).NotEmpty();
        Include(new InsuranceIdValidator(_insuranceRepository, _QueryStringResolver));
        Include(new IServiceIdValidator(_serviceRepository, _QueryStringResolver));
        Include(new IReceptionIdValidator(_receptionRepository, _QueryStringResolver));
    }
}
