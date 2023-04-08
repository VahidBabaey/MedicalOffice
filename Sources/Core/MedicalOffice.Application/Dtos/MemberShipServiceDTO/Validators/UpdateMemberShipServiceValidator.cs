using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators
{
    public class UpdateMemberShipServiceValidator : AbstractValidator<UpdateMemberShipServiceDTO>
    {
        private readonly IMembershipRepository _memberRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;
        public UpdateMemberShipServiceValidator(IServiceRepository serviceRepository, IMembershipRepository memberRepository, IContextResolver officeResolver)
        {
            _serviceRepository = serviceRepository;
            _memberRepository = memberRepository;
            _officeResolver = officeResolver;
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100)
                .WithMessage("مقدار کد تخفیف باید برابر یا کمتر از 100 باشد");
            Include(new IMembershipIdValidator(_memberRepository, _officeResolver));
            Include(new IServiceIdValidator(_serviceRepository, _officeResolver));
        }
    }
}