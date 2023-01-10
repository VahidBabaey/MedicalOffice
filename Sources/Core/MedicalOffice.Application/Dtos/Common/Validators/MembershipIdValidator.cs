using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Commons;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.CommonValidators
{
    public class MembershipIdValidator : AbstractValidator<IMembershipIdDTO>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IOfficeResolver _officeResolver;

        public MembershipIdValidator(IMembershipRepository membershipRepository, IOfficeResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _membershipRepository = membershipRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.MembershipId)
                .NotEmpty()
                .MustAsync(async (membershipId, token) =>
                {
                    var leaveTypeExists = await _membershipRepository.CheckExistMembershipId(membershipId, officeId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
