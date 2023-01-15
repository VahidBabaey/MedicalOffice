using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators
{
    public class AddMemberShipServiceValidator : AbstractValidator<MemberShipServiceDTO>
    {
        private readonly IMembershipRepository _memberRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IOfficeResolver _officeResolver;
        public AddMemberShipServiceValidator(IMembershipRepository memberRepository, IServiceRepository serviceRepository, IOfficeResolver officeResolver)
        {
            _memberRepository = memberRepository;
            _serviceRepository = serviceRepository;
            _officeResolver = officeResolver;
            RuleFor(x => x.Discount).NotEmpty();
            //Include(new ServiceListIdValidator(_serviceRepository, _officeResolver));
            Include(new MembershipIdValidator(_memberRepository, _officeResolver));
        }
    }
}
