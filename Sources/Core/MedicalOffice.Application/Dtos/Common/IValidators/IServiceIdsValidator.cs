using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IServiceIdsValidator : AbstractValidator<IServiceIdsDTO>
    {
        private readonly IContextResolver _officeResolver;
        private readonly IServiceRepository _serviceRepository;

        public IServiceIdsValidator(IContextResolver officeResolver, IServiceRepository serviceRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ServiceIds)
                .NotEmpty()
                .WithMessage("ورود شناسه خدمت ضروری است")
                .MustAsync(async (serviceIds, token) =>
                {
                    foreach (var item in serviceIds)
                    {
                        var isServiceExist = await _serviceRepository.CheckExistServiceId(officeId, item);
                        if (!isServiceExist)
                        {
                            return false;
                        }
                    }
                    return true;
                })
                //.WithMessage("{PropertyName} has some invalid values");
                .WithMessage("شناسه خدمت دارای مقدارهای نامعتبر میباشد");
        }
    }
}
