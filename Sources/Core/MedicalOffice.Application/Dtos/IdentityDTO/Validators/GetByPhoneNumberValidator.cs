using FluentValidation;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class GetByPhoneNumberValidator: AbstractValidator<GetByPhoneNumberDTO>
    {
        public GetByPhoneNumberValidator()
        {

        }
    }
}