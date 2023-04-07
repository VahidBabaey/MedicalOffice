using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.SectionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IMembershipIdValidator : AbstractValidator<IMembershipIdDTO>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IContextResolver _officeResolver;

        public IMembershipIdValidator(IMembershipRepository membershipRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _membershipRepository = membershipRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.MembershipId)
                .NotEmpty()
                .WithMessage("ورود شناسه عضویت ضروری است")
                .MustAsync(async (membershipId, token) =>
                {
                    var leaveTypeExists = await _membershipRepository.CheckExistMembershipId(officeId, membershipId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                //.WithMessage("{PropertyName} isn't exist");
                .WithMessage("شناسه عضویت موجود نیست");
        }
    }
}
