using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common.IValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators
{
    public class UpdateMedicalStaffValidator : AbstractValidator<UpdateMedicalStaffDTO>
    {
        public UpdateMedicalStaffValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x)
                .Must(x => isRoleValid(x.RoleId, x.IsTechnicalAssistant, x.IsSpecialist))
                .WithMessage("نقش های دیگر به جز پزشک نمی توانند متخصص یا مسئول فنی باشند.");
            RuleFor(x => x)
                .Must(x => isSecretoryValid(x.RoleId, x.SpecializationId))
                .WithMessage("نقش منشی باید فاقد پارامتر تخصص باشد.");

            Include(new INationalIdValidator());
            Include(new IPhoneNumberValidator());
        }

        private bool isRoleValid(Guid roleId, bool isTechnicalAssistant, bool isSpecialist)
        {
            if (roleId != DoctorRole.Id)
            {
                return isTechnicalAssistant == true || isSpecialist == true ? false : true;
            }
            return true;
        }

        private bool isSecretoryValid(Guid roleId, Guid? specializationId)
        {
            if (roleId == SecretaryRole.Id)
            {
                return specializationId != null ? false : true;
            }

            return true;
        }
    }
}
