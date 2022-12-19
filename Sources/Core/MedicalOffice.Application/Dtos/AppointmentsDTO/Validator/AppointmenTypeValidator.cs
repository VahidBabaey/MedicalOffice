using FluentValidation;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class AppointmenTypeValidator : AbstractValidator<AppointmentTypeDTO>
    {
        public AppointmenTypeValidator()
        {
            var validTypesToUpadete= new AppointmentType[] { AppointmentType.FinalApproval, AppointmentType.Canceled};

            RuleFor(x => x.AppointmentType)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must(m => validTypesToUpadete.Contains(m))
                .WithMessage("AppointmentType to change should be choosen between finalApproval or Canceled type");
        }
    }
}
