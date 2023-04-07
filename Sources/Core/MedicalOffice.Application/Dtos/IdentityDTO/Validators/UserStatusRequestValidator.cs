using FluentValidation;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalOffice.Application.Dtos.IdentityDTO.Validators
{
    public class UserStatusRequestValidator: AbstractValidator<UserStatusRequestDTO>
    {

    }
}