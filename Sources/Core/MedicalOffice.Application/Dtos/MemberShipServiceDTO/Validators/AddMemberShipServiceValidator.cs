﻿using FluentValidation;
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
        private readonly IRouteResolver _officeResolver;
        public AddMemberShipServiceValidator(IServiceRepository serviceRepository, IMembershipRepository memberRepository, IRouteResolver officeResolver)
        {
            _serviceRepository = serviceRepository;
            _memberRepository = memberRepository;
            _officeResolver = officeResolver;

            RuleFor(x => x.Discount).LessThanOrEqualTo(100);
            Include(new MembershipIdValidator(_memberRepository, _officeResolver));
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));
        }
    }
}
