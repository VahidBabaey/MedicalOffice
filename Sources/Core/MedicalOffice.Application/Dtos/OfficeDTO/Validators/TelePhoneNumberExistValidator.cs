using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.OfficeDTO.Validators
{
    public class TelePhoneNumberExistValidator : AbstractValidator<ITelePhoneNumberDTO>
    {
        private readonly IOfficeRepository _officeRepository;
        public TelePhoneNumberExistValidator(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;

            RuleFor(x => x.TelePhoneNumber)
                .MustAsync(async (phone, token) =>
                {
                    var isTelePhoneNumberExist =await _officeRepository.isTelePhoneNumberExist(phone);
                    return !isTelePhoneNumberExist;
                });
        }
    }
}
