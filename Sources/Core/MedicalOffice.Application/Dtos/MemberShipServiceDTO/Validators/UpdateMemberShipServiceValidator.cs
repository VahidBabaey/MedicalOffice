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
    public class UpdateMemberShipServiceValidator : AbstractValidator<UpdateMemberShipServiceDTO>
    {
        private readonly IMembershipRepository _memberRepository;
        private readonly IOfficeResolver _officeResolver;
        public UpdateMemberShipServiceValidator(IMembershipRepository memberRepository, IOfficeResolver officeResolver)
        {
            _memberRepository = memberRepository;
            _officeResolver = officeResolver;
            RuleFor(x => x.Discount).NotEmpty();
            Include(new MembershipIdValidator(_memberRepository, _officeResolver));
        }
    }
}
